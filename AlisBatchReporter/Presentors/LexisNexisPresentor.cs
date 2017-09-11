using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AlisBatchReporter.Classes;
using AlisBatchReporter.Views;
using System.Windows.Forms;

namespace AlisBatchReporter.Presentors
{
    internal class LexisNexisPresentor
    {
        private readonly ILexisNexisView _view;
        private Dictionary<string, string[]> _sourceDictionary;
        private Dictionary<string, string[]> _outboundDictionary;
        private readonly Dictionary<string, List<string>> _diffDictionary = new Dictionary<string, List<string>>();

        public LexisNexisPresentor(ILexisNexisView view)
        {
            _view = view;
            _view.NexisLexisValidated += ValidateLexisNexis;
        }

        private async void ValidateLexisNexis()
        {
            //Copy Files
            _view.LogProcess("Copy Files Asynch (!)...", false);
            //await CopyFiles();
            var copyTasks = new Task[2];
            copyTasks[0] = CopyFileAsync(@"\\dmfdwh001ut\E\Deploy\Prod\FTP\Validation\Lexis_Nexis\SSN_LEXIS_NEXIS.TXT",
                Path.Combine(Directory.GetCurrentDirectory(), "LN_Source.txt"));
            copyTasks[1] = CopyFileAsync(@"\\dmfdwh001ut\E\Deploy\Prod\FTP\Outbound\SSN_Feed\SSN_Feed.txt",
                Path.Combine(Directory.GetCurrentDirectory(), "LN_Outbound.txt"));
            await Task.WhenAll(copyTasks);
            _view.LogProcess("Done!", true);

            // Reading & Splitting
            _view.LogProcess("Reading Files...", false);
            await ReadFiles();
            _view.LogProcess("Done!", true);

            // Validing
            _view.LogProcess("Validating...", false);
            //Validating();
            await Validating();
            _view.LogProcess("Done!", true);

            // Writing File
            _view.LogProcess("Writing File...", false);
            await WritingFile();
            _view.LogProcess("Done!", true);

            // Deleting Source/Outbound Files
            DeleteFiles();
        }

        private void DeleteFiles()
        {
            File.Delete(Directory.GetCurrentDirectory() + @"\LN_Source.txt");
            File.Delete(Directory.GetCurrentDirectory() + @"\LN_Outbound.txt");
        }

        public async Task CopyFileAsync(string sourcePath, string destinationPath)
        {
            using (Stream source = File.Open(sourcePath, FileMode.Open, FileAccess.Read))
            {
                using (Stream destination = File.Create(destinationPath))
                {
                    await source.CopyToAsync(destination);
                }
            }
        }

        private async Task ReadFiles()
        {
            //Read Source File And Split
            _sourceDictionary = new Dictionary<string, string[]>();
            var sourceFileRead =
                await Task.Run(() => File.ReadAllLines(Directory.GetCurrentDirectory() + @"\LN_Source.txt"));
            foreach (var sourceRow in sourceFileRead)
            {
                var splitted = sourceRow.Split(',');
                var trimmed = splitted.Select(d => d.Trim()).ToArray();
                try
                {
                    // Creating Dic with Uniqe Key
                    _sourceDictionary.Add($@"{trimmed[1]}-{trimmed[2]}-{trimmed[4]}-{trimmed[6]}", trimmed);
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine(
                        $@"Error {DateTimeOffset.Now}: {e.Message}, Value: {trimmed[1]}-{trimmed[2]}-{trimmed[4]}-{
                                trimmed[6]
                            }");
                    throw;
                }
            }

            //Read Outbound File And Split
            _outboundDictionary = new Dictionary<string, string[]>();
            var outboundFileRead =
                await Task.Run(() => File.ReadAllLines(Directory.GetCurrentDirectory() + @"\LN_Outbound.txt"));
            foreach (var outboundRow in outboundFileRead)
            {
                var splitted = outboundRow.Split(',');
                var trimmed = splitted.Select(d => d.Trim()).ToArray();
                var key = $@"{trimmed[1]}-{trimmed[2]}-{trimmed[4]}-{trimmed[6]}";
                if (_outboundDictionary.ContainsKey(key))
                {
                    if (_diffDictionary.ContainsKey("Duplicate Rows - Outbound"))
                    {
                        _diffDictionary["Duplicate Rows - Outbound"].Add(key);
                    }
                    else
                    {
                        _diffDictionary.Add("Duplicate Rows - Outbound", new List<string> { key });
                    }
                    continue;
                }
                _outboundDictionary.Add(key, trimmed);               
            }
        }

        private async Task Validating()
        {
            await Task.Run(() =>
            {
                foreach (var sourceKey in _sourceDictionary.Keys)
                {
                    _outboundDictionary.TryGetValue(sourceKey, out var outboundEntry);
                    if (outboundEntry == null)
                    {
                        if (_diffDictionary.ContainsKey("[InSourceNotInOutbound]"))
                        {
                            _diffDictionary["[InSourceNotInOutbound]"].Add(sourceKey);
                        }
                        else
                        {
                            _diffDictionary.Add("[InSourceNotInOutbound]", new List<string> {sourceKey});
                        }
                    }                 
                    else
                    {
                        var sourceEntry = _sourceDictionary[sourceKey];
                        if (sourceEntry.Length > 18)
                        {
                            if (_diffDictionary.ContainsKey("Longer Rows"))
                            {
                                _diffDictionary["Longer Rows"].Add(sourceKey);
                            }
                            else
                            {
                                _diffDictionary.Add("Longer Rows", new List<string> { sourceKey });
                            }
                            continue;
                        }
                        for (int i = 0; i < sourceEntry.Length; i++)
                        {
                            LexisNexisValues fieldValue;
                            try
                            {
                                fieldValue = LexisNexisValues.GetValue(i);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e);
                                throw;
                            }
                            
                            if (fieldValue.ToIgnore)
                            {
                                continue;
                            }
                            if (fieldValue.Intable)
                            {
                                long.TryParse(sourceEntry[fieldValue.IdxValue], out var intSource);
                                long.TryParse(outboundEntry[fieldValue.IdxValue], out var intOutbound);
                                sourceEntry[fieldValue.IdxValue] = intSource.ToString();
                                outboundEntry[fieldValue.IdxValue] = intOutbound.ToString();
                            }
                            if (!sourceEntry[fieldValue.IdxValue].Equals(outboundEntry[fieldValue.IdxValue]))
                            {
                                if (_diffDictionary.ContainsKey($@"[{fieldValue.Name}]"))
                                {
                                    _diffDictionary[$@"[{fieldValue.Name}]"].Add(sourceKey +
                                                                                 $@"  LifeComm Value: {
                                                                                         sourceEntry[
                                                                                             fieldValue.IdxValue]
                                                                                     }" +
                                                                                 $@"  DMFI Value: {
                                                                                         outboundEntry[
                                                                                             fieldValue.IdxValue]
                                                                                     }");
                                }
                                else
                                {
                                    _diffDictionary.Add($@"[{fieldValue.Name}]", new List<string>
                                    {
                                        sourceKey + $@"  LifeComm Value: {sourceEntry[fieldValue.IdxValue]}" +
                                        $@"  DMFI Value: {outboundEntry[fieldValue.IdxValue]}"
                                    });
                                }
                            }
                        }
                    }
                }
            });
        }

        private async Task WritingFile()
        {
            if (!Directory.Exists(Path.GetDirectoryName(Application.ExecutablePath)
                                 + @"\Output\LexisNexis"))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(Application.ExecutablePath)
                                          + @"\Output\LexisNexis");
            }           
            using (StreamWriter file = new StreamWriter(Path.GetDirectoryName(Application.ExecutablePath)
                + $@"\Output\LexisNexis\LexisNexis_Diffs_{DateTime.Now:yyyy-dd-M--HH-mm-ss}.txt"))
                foreach (var entry in _diffDictionary)
                {
                    file.WriteLine($@"[{entry.Key}]");
                    foreach (var val in entry.Value)
                    {
                        await file.WriteLineAsync(val);
                    }
                }
        }
    }
}
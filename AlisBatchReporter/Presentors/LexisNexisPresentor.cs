using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AlisBatchReporter.Views;

namespace AlisBatchReporter.Presentors
{
    class LexisNexisPresentor
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
            _view.LogProcess("Copy Files...", true);
            await CopyFiles();
            _view.LogProcess("Done!", true);

            // Reading & Splitting
            _view.LogProcess("Reading Files...", false);
            await ReadFiles();
            _view.LogProcess("Done!", true);

            // Validing
            _view.LogProcess("Validating...", false);
            Validating();
            _view.LogProcess("Done!", true);

            // Writing File
            _view.LogProcess("Writing File...", false);
            WritingFile();
            _view.LogProcess("Done!", true);

            // Deleting Source/Outbound Files
            DeleteFiles();
        }

        private void DeleteFiles()
        {
            File.Delete(Directory.GetCurrentDirectory() + @"\LN_Source.txt");
            File.Delete(Directory.GetCurrentDirectory() + @"\LN_Outbound.txt");
        }

        private async Task CopyFiles()
        {
            //Copy Source
            _view.LogProcess("Copy File: SSN_LEXIS_NEXIS_JULY9.TXT...", false);
            await Task.Run(() => File.Copy(
                @"\\dmfdwh001pr\X\Deploy\Prod\FTP\Validation\Lexis_Nexis\SSN_LEXIS_NEXIS_JULY9.TXT",
                Path.Combine(Directory.GetCurrentDirectory(), "LN_Source.txt"), true));
            _view.LogProcess("Done!", true);

            //Copy Outbound
            _view.LogProcess("Copy File: SSN_Feed.txt", false);
            await Task.Run(() => File.Copy(@"\\dmfdwh001pr\X\Deploy\Prod\FTP\Outbound\SSN_Feed\SSN_Feed.txt",
                Path.Combine(Directory.GetCurrentDirectory(), "LN_Outbound.txt"), true));
            _view.LogProcess("Done!", true);
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
                    _sourceDictionary.Add($@"{trimmed[1]}-{trimmed[2]}-{trimmed[6]}", trimmed);
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine($@"Error {DateTimeOffset.Now}: {e.Message}, Value: {trimmed[1]}-{trimmed[2]}-{trimmed[6]}");
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
                _outboundDictionary.Add($@"{trimmed[1]}-{trimmed[2]}-{trimmed[6]}", trimmed);
            }
        }

        private void Validating()
        {
            foreach (var sourceKey in _sourceDictionary.Keys)
            {
                string[] outboundEntry;
                _outboundDictionary.TryGetValue(sourceKey, out outboundEntry);
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
                    for (int i = 0; i < sourceEntry.Length; i++)
                    {
                        var idxName = (LexisNexisEnum) i;
                        if (!sourceEntry[i].Equals(outboundEntry[i]))
                        {
                            if (_diffDictionary.ContainsKey($@"[{idxName}]"))
                            {
                                _diffDictionary[$@"[{idxName}]"].Add(sourceKey +
                                                                     $@"  LifeComm Value: {sourceEntry[i]}" +
                                                                     $@"  DMFI Value: {outboundEntry[i]}");
                            }
                            else
                            {
                                _diffDictionary.Add($@"[{idxName}]", new List<string>
                                {
                                    sourceKey + $@"  LifeComm Value: {sourceEntry[i]}" +
                                    $@"  DMFI Value: {outboundEntry[i]}"
                                });
                            }
                        }
                    }
                }
            }
        }

        private void WritingFile()
        {
            using (StreamWriter file = new StreamWriter("LexisNexis_Diffs.txt"))
                foreach (var entry in _diffDictionary)
                {
                    file.WriteLineAsync($@"[{entry.Key}]");
                    entry.Value.ForEach(policy => file.WriteLine(policy));
                }
        }
    }

    public enum LexisNexisEnum
    {
        SSN = 0,
        PolicyNumber = 1,
        FirstName = 2,
        MiddleInitial = 3,
        LastName = 4,
        NameSuffix = 5,
        DOB = 6,
        StreetAddress1 = 7,
        StreetAddress2 = 8,
        StreetAddress3 = 9,
        StreetAddress4 = 10,
        City = 11,
        State = 12,
        Zip = 13,
        PolicyStatus = 14,
        ServicingAgentNumber = 15,
        PolicyIssueDate = 16,
        PolicyTerminationDate = 17
    }
}
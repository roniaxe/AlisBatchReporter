using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using AlisBatchReporter.Classes;
using AlisBatchReporter.Views;

namespace AlisBatchReporter.Presentors
{
    internal class ArcvalPresentor
    {
        private readonly IArcvalView _arcvalView;
        private readonly Dictionary<string, List<string>> _diffDictionary = new Dictionary<string, List<string>>();
        private readonly Dictionary<string, string> _outboundDictionary = new Dictionary<string, string>();
        private readonly Dictionary<string, string> _sourceDictionary = new Dictionary<string, string>();


        public ArcvalPresentor(IArcvalView arcvalView)
        {
            _arcvalView = arcvalView;
            _arcvalView.Compared += CompareArcval;
        }

        private async void CompareArcval()
        {
            if (_arcvalView.CopyFiles)
            {
                _arcvalView.LogProcess("Copy Files Asynch (!)...", false);
                //await CopyFiles();
                var copyTasks = new Task[2];

                copyTasks[0] = CopyFileAsync(
                    @"\\dmfdwh001pr\X\Deploy\Prod\FTP\Validation\ARCVAL\ARCVAL.TXT",
                    Path.Combine(Directory.GetCurrentDirectory(), "AV_Source.txt"));

                copyTasks[1] = CopyFileAsync(
                    @"\\dmfdwh001pr\X\Deploy\Prod\FTP\Outbound\ARCVAL\ARCVAL_Traditional.txt",
                    Path.Combine(Directory.GetCurrentDirectory(), "AV_Outbound.txt"));

                await Task.WhenAll(copyTasks);
                _arcvalView.LogProcess("Done!", true);
            }

            // Reading & Splitting
            _arcvalView.LogProcess("Reading Files...", false);
            var sourceContent = ReadFiles("AV_Source.txt");
            var outboundContent = ReadFiles("AV_Outbound.txt");
            await Task.WhenAll(sourceContent, outboundContent);
            _arcvalView.LogProcess("Done!", true);

            //Indexing
            _arcvalView.LogProcess("Indexing Source...", false);
            var inactivePolList = await IndexingSource(await sourceContent);
            _arcvalView.LogProcess("Done!", true);
            _arcvalView.LogProcess("Indexing Outbound...", false);
            await IndexingOutbound(await outboundContent, inactivePolList);
            _arcvalView.LogProcess("Done!", true);

            // Validing
            _arcvalView.LogProcess("Validating...", false);
            await Validating();
            _arcvalView.LogProcess("Done!", true);

            // Writing File
            _arcvalView.LogProcess("Writing File...", false);
            await WritingFile();
            _arcvalView.LogProcess("Done!", true);

            // Deleting Source/Outbound Files
            DeleteFiles();
        }

        private async Task CopyFileAsync(string sourcePath, string destinationPath)
        {
            using (Stream source = File.Open(sourcePath, FileMode.Open, FileAccess.Read))
            {
                using (Stream destination = File.Create(destinationPath))
                {
                    await source.CopyToAsync(destination);
                }
            }
        }

        private async Task<string> ReadFiles(string fileName)
        {
            using (var sourceReader = File.OpenText(Directory.GetCurrentDirectory() + $@"\{fileName}"))
            {
                return await sourceReader.ReadToEndAsync();
            }
        }

        private async Task<List<string>> IndexingSource(string content)
        {
            List<string> inactiveList = null;
            //Read Source File And Split
            await Task.Run(() =>
            {
                var sourceMotation = content.Replace(Convert.ToChar(0x0).ToString(), " ")
                    .Split(new[] {Environment.NewLine}, StringSplitOptions.None);
                inactiveList = new List<string>();
                foreach (var sourceRow in sourceMotation)
                {
                    if (sourceRow.Length < 30)
                        continue;

                    var rowType = sourceRow.Substring(42, 2);
                    if (rowType[0].Equals('1') && !rowType[1].Equals('1'))
                    {
                        inactiveList.Add(sourceRow.Substring(30, 9));
                        continue;
                    }
                    var polNo = sourceRow.Substring(30, 9);
                    if (inactiveList.Contains(polNo))
                        continue;
                    var key = sourceRow.Substring(30, 13);
                    if (rowType[0].Equals('5'))
                    {
                        var secondKey = sourceRow.Substring(55, 15);
                        key += $@"-{secondKey}";
                    }

                    try
                    {
                        // Creating Dic with Uniqe Key
                        _sourceDictionary.Add($@"{key}", sourceRow);
                    }
                    catch (ArgumentException e)
                    {
                        Console.WriteLine(
                            $@"Error {DateTimeOffset.Now}: {e.Message}, Value: {key}");
                        throw;
                    }
                }
            });
            return inactiveList;
        }

        private async Task IndexingOutbound(string content, List<string> inactiveList)
        {
            await Task.Run(() =>
            {
                var outboundSplitted = content.Split(new[] {Environment.NewLine}, StringSplitOptions.None);
                foreach (var outboundRow in outboundSplitted)
                {
                    if (outboundRow.Length < 30)
                        continue;
                    var polNo = outboundRow.Substring(30, 9);
                    if (inactiveList.Contains(polNo))
                    {
                        if (_diffDictionary.ContainsKey("Inactive Policies"))
                            _diffDictionary["Inactive Policies"].Add(polNo);
                        else
                            _diffDictionary.Add("Inactive Policies", new List<string> {polNo});
                        continue;
                    }
                    var rowType = outboundRow.Substring(42, 1);
                    var key = outboundRow.Substring(30, 13);
                    if (rowType.Equals("5"))
                    {
                        var secondKey = outboundRow.Substring(55, 15);
                        key += $@"-{secondKey}";
                    }
                    try
                    {
                        // Creating Dic with Uniqe Key
                        if (_outboundDictionary.ContainsKey(key))
                            if (_diffDictionary.ContainsKey("Duplicates"))
                                _diffDictionary["Duplicates"].Add(key);
                            else
                                _diffDictionary.Add("Duplicates", new List<string> {key});
                        else
                            _outboundDictionary.Add($@"{key}", outboundRow);
                    }
                    catch (ArgumentException e)
                    {
                        Console.WriteLine(
                            $@"Error {DateTimeOffset.Now}: {e.Message}, Value: {key}");
                        throw;
                    }
                }
            });
        }

        private async Task Validating()
        {
            await Task.Run(() =>
            {
                foreach (var sourceKey in _sourceDictionary.Keys)
                {
                    string outboundEntry;
                    _outboundDictionary.TryGetValue(sourceKey, out outboundEntry);
                    if (outboundEntry == null)
                    {
                        if (_diffDictionary.ContainsKey("[InSourceNotInOutbound]"))
                            _diffDictionary["[InSourceNotInOutbound]"].Add(sourceKey);
                        else
                            _diffDictionary.Add("[InSourceNotInOutbound]", new List<string> {sourceKey});
                    }
                    else
                    {
                        var sourceEntry = _sourceDictionary[sourceKey];
                        sourceEntry = sourceEntry.TrimEnd();
                        outboundEntry = outboundEntry.TrimEnd();
                        if (outboundEntry.Length != sourceEntry.Length)
                        {
                            if (_diffDictionary.ContainsKey("[Different Row Length]"))
                                _diffDictionary["[Different Row Length]"].Add(sourceKey);
                            else
                                _diffDictionary.Add("[Different Row Length]", new List<string> {sourceKey});
                        }
                        else
                        {
                            string type = null;
                            int[] cuts = { };
                            if (sourceEntry.Length == 100)
                            {
                                cuts = new[]
                                    {2, 14, 15, 16, 18, 19, 20, 28, 30, 42, 43, 44, 46, 47, 56, 64, 73, 82, 90, 91};
                                type = "Type1";
                            }
                            if (sourceEntry.Length == 72)
                            {
                                cuts = new[] {2, 14, 15, 16, 18, 19, 20, 28, 30, 42, 43, 44, 46, 48};
                                type = "Type7";
                            }
                            if (sourceEntry.Length == 177)
                            {
                                cuts = new[]
                                {
                                    2, 14, 15, 16, 18, 19, 20, 28, 30, 42, 43, 44, 56, 57, 69, 71, 77, 85, 93,
                                    101, 110, 119
                                };
                                type = "Type6A";
                            }
                            if (sourceEntry.Length == 195)
                            {
                                cuts = new[]
                                {
                                    2, 14, 15, 16, 18, 19, 20, 28, 30, 42, 43, 55, 56, 57, 58, 60, 62, 70, 78,
                                    101, 110, 119, 128, 137, 146
                                };
                                type = "Type5";
                            }
                            var sourceSplitted = SplitAt(sourceEntry, cuts);
                            var outboundSplitted = SplitAt(outboundEntry, cuts);
                            Compare(sourceSplitted, outboundSplitted, type, sourceKey);
                        }
                    }
                }
            });
        }

        private void Compare(IReadOnlyList<string> sourceSplitted, IReadOnlyList<string> outboundSplitted, string type, string key)
        {
            Values values = null;
            switch (type)
            {
                case "Type1":
                    values = new ArcvalValues();
                    break;
                case "Type7":
                    values = new ArcvalValuesType7();
                    break;
                case "Type6A":
                    values = new ArcvalValuesType6A();
                    break;
                case "Type5":
                    values = new ArcvalValuesType5();
                    break;
            }
            for (var i = 0; i < sourceSplitted.Count - 1; i++)
            {
                var idxName = values?.GetValue(i);

                if (idxName != null && !sourceSplitted[i].Equals(outboundSplitted[i]) && !idxName.ToIgnore)
                    if (_diffDictionary.ContainsKey(idxName.Name))
                        _diffDictionary[idxName.Name]
                            .Add(key + $@" - Source Val: {sourceSplitted[i]}, Outbound Val: {outboundSplitted[i]}");
                    else
                        _diffDictionary.Add(idxName.Name,
                            new List<string>
                            {
                                key + $@" - Source Val: {sourceSplitted[i]}, Outbound Val: {outboundSplitted[i]}"
                            });
            }
        }

        public string[] SplitAt(string source, params int[] index)
        {
            index = index.Distinct().OrderBy(x => x).ToArray();
            var output = new string[index.Length + 1];
            var pos = 0;

            for (var i = 0; i < index.Length; pos = index[i++])
                output[i] = source.Substring(pos, index[i] - pos);

            output[index.Length] = source.Substring(pos);
            return output;
        }

        private async Task WritingFile()
        {
            if (!Directory.Exists(Path.GetDirectoryName(Application.ExecutablePath)
                                  + @"\Output\ARCVAL"))
                Directory.CreateDirectory(Path.GetDirectoryName(Application.ExecutablePath)
                                          + @"\Output\ARCVAL");
            using (var file = new StreamWriter(Path.GetDirectoryName(Application.ExecutablePath)
                                               + $@"\Output\ARCVAL\ARCVAL_Diffs_{DateTime.Now:yyyy-dd-M--HH-mm-ss}.txt")
            )
            {
                foreach (var entry in _diffDictionary)
                {
                    file.WriteLine($@"[{entry.Key}]");
                    foreach (var val in entry.Value)
                        await file.WriteLineAsync(val);
                }
            }
        }

        private void DeleteFiles()
        {
            File.Delete(Directory.GetCurrentDirectory() + @"\AV_Source.txt");
            File.Delete(Directory.GetCurrentDirectory() + @"\AV_Outbound.txt");
        }
    }
}
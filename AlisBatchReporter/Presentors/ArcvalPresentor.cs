using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using AlisBatchReporter.Views;

namespace AlisBatchReporter.Presentors
{
    internal class ArcvalPresentor
    {
        private readonly IArcvalView _arcvalView;
        private readonly Dictionary<string, List<string>> _diffDictionary = new Dictionary<string, List<string>>();
        private readonly HashSet<string> _inactivePolicies = new HashSet<string>();
        private readonly Dictionary<string, ArcvalInstance> _outboundDictionary = new Dictionary<string, ArcvalInstance>();
        private readonly Dictionary<string, ArcvalInstance> _sourceDictionary = new Dictionary<string, ArcvalInstance>();
        private CancellationTokenSource _cancellationTokenSource;


        public ArcvalPresentor(IArcvalView arcvalView)
        {
            _arcvalView = arcvalView;
            _arcvalView.Compared += CompareArcval;
            _arcvalView.Cancelled += CancelCompare;
            _arcvalView.OverrideFilesChecked += OverrideFiles;
        }

        private void CancelCompare()
        {
            _cancellationTokenSource.Cancel();
        }

        private void OverrideFiles()
        {
            _arcvalView.OverridePanel.Visible = _arcvalView.OverrideFiles;
        }

        private async void CompareArcval()
        {
            _arcvalView.DisabledCompareButton();
            _arcvalView.EnabledCancelButton();
            if (_arcvalView.CopyFiles)
            {
                _arcvalView.LogProcess("Copy Files Asynch (!)...", false);
                //await CopyFiles();
                var copyTasks = new Task[2];

                var sourceFileName = !string.IsNullOrEmpty(_arcvalView.SourceFileName)
                    ? _arcvalView.SourceFileName
                    : "ARCVAL.TXT";

                var outboundFileName = !string.IsNullOrEmpty(_arcvalView.OutboundFileName)
                    ? _arcvalView.OutboundFileName
                    : "ARCVAL_Traditional.txt";

                var envPath = _arcvalView.ProdRadioButton ? @"\\dmfdwh001pr\X" : @"\\dmfdwh001ut\E";

                copyTasks[0] = CopyFileAsync(
                    $@"{envPath}\Deploy\Prod\FTP\Validation\ARCVAL\{sourceFileName}",
                    Path.Combine(Directory.GetCurrentDirectory(), "AV_Source.txt"));

                copyTasks[1] = CopyFileAsync(
                    $@"{envPath}\Deploy\Prod\FTP\Outbound\ARCVAL\{outboundFileName}",
                    Path.Combine(Directory.GetCurrentDirectory(), "AV_Outbound.txt"));

                await Task.WhenAll(copyTasks);
                _arcvalView.LogProcess("Done!", true);
            }

            // Reading & Splitting
            _arcvalView.LogProcess("Reading Files Asynch (!)...", false);
            var sourceContent = ReadFiles("AV_Source.txt");
            var outboundContent = ReadFiles("AV_Outbound.txt");
            await Task.WhenAll(sourceContent, outboundContent);
            _arcvalView.LogProcess("Done!", true);

            //Indexing
            _arcvalView.LogProcess("Indexing Source...", false);
            await IndexingSource(await sourceContent);
            _arcvalView.LogProcess("Done!", true);
            _arcvalView.LogProcess("Indexing Outbound...", false);
            await IndexingOutbound(await outboundContent);
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
            _arcvalView.DisabledCompareButton();
            _arcvalView.DisabledCancelButton();
        }

        private async Task CopyFileAsync(string sourcePath, string destinationPath)
        {
            _cancellationTokenSource = new CancellationTokenSource();
            using (Stream source = File.Open(sourcePath, FileMode.Open, FileAccess.Read))
            {
                using (Stream destination = File.Create(destinationPath))
                {
                    await source.CopyToAsync(destination, 81920, _cancellationTokenSource.Token);
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

        private async Task IndexingSource(string content)
        {
            _cancellationTokenSource = new CancellationTokenSource();
            //Read Source File And Split
            await Task.Run(() =>
            {
                var sourceMotation = content.Replace(Convert.ToChar(0x0).ToString(), " ")
                    .Split(new[] {Environment.NewLine}, StringSplitOptions.None);
                foreach (var sourceRow in sourceMotation)
                {
                    var arcval = ArcvalFactory.GetArcvalInstance(sourceRow);

                    if (!arcval.Valid) continue;

                    if (_inactivePolicies.Contains(arcval.PolicyNo)) continue;

                    if (arcval.Type == ArcvalRowType.BaseCover &&
                        arcval.Status == ArcvalRowStatus.Inactive)
                    {
                        _inactivePolicies.Add(arcval.PolicyNo);
                        continue;
                    }

                    try
                    {
                        AddToSrcDic(arcval, "Duplicates in source");
                    }
                    catch (ArgumentException e)
                    {
                        Console.WriteLine(
                            $@"Error {DateTimeOffset.Now}: {e.Message}, Value: {arcval.Key}");
                        throw;
                    }
                }
            }, _cancellationTokenSource.Token);
        }

        private void AddToSrcDic(ArcvalInstance arcval, string dicKey)
        {
            if (_sourceDictionary.ContainsKey(arcval.Key))
                if (_diffDictionary.ContainsKey(dicKey))
                    _diffDictionary[dicKey].Add(arcval.Key);
                else
                    _diffDictionary.Add(dicKey, new List<string> {arcval.Key});
            else
                _sourceDictionary.Add(arcval.Key, arcval);
        }

        private async Task IndexingOutbound(string content)
        {
            _cancellationTokenSource = new CancellationTokenSource();
            await Task.Run(() =>
            {
                var outboundSplitted = content.Split(new[] {Environment.NewLine}, StringSplitOptions.None);
                foreach (var outboundRow in outboundSplitted)
                {
                    var arcval = ArcvalFactory.GetArcvalInstance(outboundRow);
                    if (!arcval.Valid)
                        continue;
                    if (_inactivePolicies.Contains(arcval.PolicyNo)) continue;
                    try
                    {
                        AddToObDic(arcval, "Duplicates in outbound");
                    }
                    catch (ArgumentException e)
                    {
                        Console.WriteLine(
                            $@"Error {DateTimeOffset.Now}: {e.Message}, Value: {arcval.Key}");
                        throw;
                    }
                }
            }, _cancellationTokenSource.Token);
        }

        private void AddToObDic(ArcvalInstance arcval, string dicKey)
        {
            if (_outboundDictionary.ContainsKey(arcval.Key))
                if (_diffDictionary.ContainsKey(dicKey))
                    _diffDictionary[dicKey].Add(arcval.Key);
                else
                    _diffDictionary.Add(dicKey, new List<string> {arcval.Key});
            else
                _outboundDictionary.Add(arcval.Key, arcval);
        }

        private async Task Validating()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            await Task.Run(() =>
            {
                foreach (var source in _sourceDictionary)
                {
                    _outboundDictionary.TryGetValue(source.Key, out var outboundEntry);
                    if (outboundEntry == null)
                    {
                        AddToDiffDic(source.Value, null, "[InSourceNotInOutbound]", null);
                    }
                    else
                    {
                        if (outboundEntry.ArcvalProps.Count != source.Value.ArcvalProps.Count)
                        {
                            AddToDiffDic(source.Value, null, "[Different Row Length]", null);
                        }
                        else
                        {
                            Compare(source.Value, outboundEntry);
                        }
                    }
                }
            }, _cancellationTokenSource.Token);
        }

        private void AddToDiffDic(ArcvalInstance source, ArcvalInstance outbound, string dicKey, int? index)
        {
            var val = source.Type == ArcvalRowType.BaseCover || source.Type == ArcvalRowType.Rpu
                ? $@"{source.Key} ({source.Status.ToString()})"
                : source.Key;
            if (outbound != null && index != null)
            {
                val = val + $@" - Source Val: {source.ArcvalProps[index.Value].Value}, Outbound Val: {
                              outbound.ArcvalProps[index.Value].Value
                          }";
            }
            if (_diffDictionary.ContainsKey(dicKey))
                _diffDictionary[dicKey].Add(val);
            else
                _diffDictionary.Add(dicKey, new List<string> { val });
        }

        private void Compare(
            ArcvalInstance sourceProps,
            ArcvalInstance outboundProps)
        {
            for (var i = 0; i < sourceProps.ArcvalProps.Count - 1; i++)
            {
                if (sourceProps.ArcvalProps[i].ToIgnore || 
                    sourceProps.ArcvalProps[i].Value.Equals(outboundProps.ArcvalProps[i].Value)) continue;

                if (sourceProps.ArcvalProps[i].Intable)
                {
                    int.TryParse(sourceProps.ArcvalProps[i].Value, out var x);
                    int.TryParse(outboundProps.ArcvalProps[i].Value, out var y);
                    if (Math.Abs(x - y) <= 5) continue;
                }
                AddToDiffDic(sourceProps, outboundProps, sourceProps.ArcvalProps[i].Name , i);
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
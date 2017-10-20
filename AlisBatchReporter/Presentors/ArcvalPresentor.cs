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
        #region Properties

        private readonly IArcvalView _arcvalView;
        private readonly Dictionary<string, ArcvalInstance> _outboundDic = new Dictionary<string, ArcvalInstance>();
        private readonly Dictionary<string, ArcvalInstance> _sourceDic = new Dictionary<string, ArcvalInstance>();
        private readonly Dictionary<string, List<string>> _diffDictionary = new Dictionary<string, List<string>>();
        private CancellationTokenSource _cancellationTokenSource;

        #endregion

        #region Constructor

        public ArcvalPresentor(IArcvalView arcvalView)
        {
            _arcvalView = arcvalView;
            _arcvalView.Compared += CompareArcval;
            _arcvalView.Cancelled += CancelCompare;
            _arcvalView.OverrideFilesChecked += OverrideFiles;
        }

        #endregion

        

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
                await CopyFiles();
            }

            // Reading & Splitting
            _cancellationTokenSource = new CancellationTokenSource();
            try
            {
                _arcvalView.LogProcess("Reading Files...", false);
                var sourceContent = ReadFiles("AV_Source.txt");
                var outboundContent = ReadFiles("AV_Outbound.txt");
                await Task.WhenAll(sourceContent, outboundContent);
                _arcvalView.LogProcess("Done!", true);

                _arcvalView.LogProcess("Indexing Source File...", false);
                await Task.Run(() => Indexing(sourceContent.Result, _sourceDic));
                _arcvalView.LogProcess("Done!", true);

                _arcvalView.LogProcess("Indexing Outbound File...", false);
                await Task.Run(() => Indexing(outboundContent.Result, _outboundDic));
                _arcvalView.LogProcess("Done!", true);               
            }
            catch (TaskCanceledException exception)
            {
                MessageBox.Show(exception.Message, @"Task Cancelled");
                _arcvalView.DisabledCancelButton();
                _arcvalView.EnabledCompareButton();
                return;
            }

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

        private async Task CopyFiles()
        {
            _arcvalView.LogProcess("Copy Files Asynch (!)...", false);
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

        private static async Task<string> ReadFiles(string fileName)
        {
            try
            {
                using (var sourceReader = File.OpenText(Directory.GetCurrentDirectory() + $@"\{fileName}"))
                {
                    return await sourceReader.ReadToEndAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        private void Indexing(string content, IDictionary<string, ArcvalInstance> dic)
        {
            ArcvalFactory.PolicyStatusDic.Clear();
            var sourceMotation = content
                .Replace(Convert.ToChar(0x0).ToString(), " ")
                .Split(new[] {Environment.NewLine}, StringSplitOptions.None);

            foreach (var sourceInstance in sourceMotation)
            {
                ProcessArcval(sourceInstance, dic);
            }
        }

        private void ProcessArcval(string arcvalRow, IDictionary<string, ArcvalInstance> dic)
        {
            var arcval = ArcvalFactory.GetArcvalInstance(arcvalRow);
            if (arcval == null) return;

            try
            {
                AddToDictionary(dic, arcval);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(
                    $@"Error {DateTimeOffset.Now}: {e.Message}, Value: {arcval.Key}");
                throw;
            }
        }

        private void AddToDictionary(IDictionary<string, ArcvalInstance> dic, ArcvalInstance arcval)
        {
            if (dic.ContainsKey(arcval.Key))
            {
                if (_diffDictionary.ContainsKey("Duplicates"))
                {
                    _diffDictionary["Duplicates"].Add(arcval.Key);
                }
                else
                {
                    _diffDictionary["Duplicates"] = new List<string>{ arcval.Key };
                }
            }
            else
            {
                dic.Add(arcval.Key, arcval);
            }
        }

        private async Task Validating()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            await Task.Run(() =>
            {
                foreach (var source in _sourceDic)
                {
                    _outboundDic.TryGetValue(source.Key, out var outboundEntry);
                    if (outboundEntry == null)
                    {
                        AddToDiffDic(source.Value, null, "[InSourceNotInOutbound]", null);
                    }
                    else
                    {
                        if (outboundEntry.ArcvalProps.Count != source.Value.ArcvalProps.Count)
                            AddToDiffDic(source.Value, null, "[Different Row Length]", null);
                        else
                            Compare(source.Value, outboundEntry);
                    }
                }
            }, _cancellationTokenSource.Token);
        }

        private void AddToDiffDic(ArcvalInstance source, ArcvalInstance outbound, string dicKey, int? index)
        {
            var val = $@"{source.Key} ({source.Status.ToString()})";

            if (outbound != null && index != null)
                val = val + $@" - Source Val: {source.ArcvalProps[index.Value].Value}, Outbound Val: {
                              outbound.ArcvalProps[index.Value].Value
                          }";
            if (_diffDictionary.ContainsKey(dicKey))
                _diffDictionary[dicKey].Add(val);
            else
                _diffDictionary.Add(dicKey, new List<string> {val});
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
                AddToDiffDic(sourceProps, outboundProps, sourceProps.ArcvalProps[i].Name, i);
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
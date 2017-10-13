using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using AlisBatchReporter.Classes;

namespace AlisBatchReporter.Forms
{
    public partial class DmfiValidationsForm : Form
    {
        private readonly ConcurrentDictionary<string, List<string>> _differencesCount =
            new ConcurrentDictionary<string, List<string>>();

        private long _current;
        private long _total;

        public DmfiValidationsForm()
        {
            InitializeComponent();
        }

        public double Progress
        {
            get
            {
                if (_total == 0)
                    return 0;
                return (double) _current / _total;
            }
        }

        private async void validateButton_Click(object sender, EventArgs e)
        {
            await ValidateAnalytic(validationTypesCombobox.Text);
        }

        private static async Task CopyFileAsync(string sourcePath, string destinationPath)
        {
            using (Stream source = File.Open(sourcePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (Stream destination = File.Create(destinationPath))
                {
                    await source.CopyToAsync(destination);
                }
            }
        }

        public async Task<string> ReadAllLinesAsync(string path, Encoding encoding)
        {
            using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read, 4096,
                FileOptions.Asynchronous))
            using (var reader = new StreamReader(stream, encoding))
            {
                return await reader.ReadToEndAsync();
            }
        }

        private async Task ValidateAnalytic(string selectedItem)
        {
            if (selectedItem.Equals("IRF2 File Comparison"))
                await Compare(2);

            if (selectedItem.Equals("IRF1 File Comparison"))
                await Compare(1);
        }

        private async Task Compare(int irfType)
        {
            DeleteFiles();
            LogProcess("Copy Files Asynch (!)...", false);
            //await CopyFiles();
            var copyTasks = new Task[2];
            var sourceFileName = $"LFCM_IRF{irfType}.txt";
            var outboundFileName = $"IRF{irfType}.txt";
            var envPath = @"\\dmfdwh001ut\E";
            copyTasks[0] = CopyFileAsync(
                $@"{envPath}\Deploy\Prod\FTP\Validation\AnalyticFeed\{sourceFileName}",
                Path.Combine(Directory.GetCurrentDirectory(), "IRF_Source.txt"));
            copyTasks[1] = CopyFileAsync(
                $@"{envPath}\Deploy\Prod\FTP\Outbound\AnalyticFeed\{outboundFileName}",
                Path.Combine(Directory.GetCurrentDirectory(), "IRF_Outbound.txt"));
            await Task.WhenAll(copyTasks);
            LogProcess("Done!", true);

            //Read New Files
            LogProcess("Reading Files Asynch (!)...", false);
            var sourceFileRead =
                ReadAllLinesAsync(Directory.GetCurrentDirectory() + @"\IRF_Source.txt", Encoding.UTF8);
            var outboundFileRead =
                ReadAllLinesAsync(Directory.GetCurrentDirectory() + @"\IRF_Outbound.txt", Encoding.UTF8);
            await Task.WhenAll(sourceFileRead, outboundFileRead);
            LogProcess("Done!", true);

            LogProcess("Processing Data...", false);
            panel1.Visible = true;
            var sourceObjects = ProcessData(await sourceFileRead, await outboundFileRead);
            await Task.WhenAll(sourceObjects);
            panel1.Visible = false;
            LogProcess("Done!", true);

            // Create Output
            LogProcess(@"Writing To File...", false);
            CreateOutputFile();
            LogProcess("Done!", true);
        }

        private async Task ProcessData(string sourceFileContent, string outboundFileContent)
        {
            // The Progress<T> constructor captures our UI context,
            //  so the lambda will be run on the UI thread.
            IProgress<int> progress = new Progress<int>(percent =>
            {
                label1.Text = percent + @"%";
                progressBar1.Value = percent;
            });

            var indexArr = new[]
            {
                3, 13, 23, 25, 35, 45, 55, 56, 59,
                69, 74, 94, 96, 97, 122, 137, 152, 162, 172, 182, 192, 202, 212, 213, 215, 217
            };

            var srcDic = new Dictionary<string, string>();
            foreach (var srcItem in sourceFileContent.Split(new[] {Environment.NewLine}, StringSplitOptions.None).Where(i => i.Any() && i[212] == 'Y'))
                if (srcItem.Length > 0)
                    if (!srcDic.ContainsKey(srcItem.Substring(3, 10) + "-" + srcItem.Substring(45, 10)))
                        srcDic.Add(srcItem.Substring(3, 10) + "-" + srcItem.Substring(45, 10), srcItem);
                    else
                        _differencesCount.AddOrUpdate("Duplicates Src",
                            new List<string> {srcItem.Substring(3, 10) + "-" + srcItem.Substring(45, 10)},
                            (dicKey, existingVal) =>
                            {
                                existingVal.Add(srcItem.Substring(3, 10) + "-" + srcItem.Substring(45, 10));
                                return existingVal;
                            });

            var srcOb = new Dictionary<string, string>();
            foreach (var obItem in outboundFileContent.Split(new[] {Environment.NewLine}, StringSplitOptions.None))
                if (obItem.Length > 0)
                    if (!srcOb.ContainsKey(obItem.Substring(3, 10) + "-" + obItem.Substring(45, 10)))
                        srcOb.Add(obItem.Substring(3, 10) + "-" + obItem.Substring(45, 10), obItem);
                    else
                        _differencesCount.AddOrUpdate("Duplicates Outbound",
                            new List<string> {obItem.Substring(3, 10) + "-" + obItem.Substring(45, 10)},
                            (dicKey, existingVal) =>
                            {
                                existingVal.Add(obItem.Substring(3, 10) + "-" + obItem.Substring(45, 10));
                                return existingVal;
                            });

            _total = srcDic.Count - 1;
            _current = 0;
            await Task.Run(() =>
            {
                Parallel.ForEach(srcDic, idx =>
                {
                    progress.Report((int) (Progress * 100));
                    if (idx.Value.Length == 0)
                        return;
                    try
                    {
                        srcOb.TryGetValue(idx.Key, out var matchedIrf);
                        if (matchedIrf == null)
                        {
                            ReportMissing(idx.Key);
                            return;
                        }

                        var bodySrc = SplitAt(idx.Value, indexArr);
                        var bodyOb = SplitAt(matchedIrf, indexArr);
                        for (var i = 0; i < bodySrc.Length - 1; i++)
                        {
                            var valueSettings = IrfPropSettings.PropSettArray[i];
                            if (valueSettings.ToIgnore) return;
                            if (valueSettings.Intable)
                            {
                                int.TryParse(bodySrc[i], out var intResult);
                                bodySrc[i] = intResult.ToString();
                                int.TryParse(bodyOb[i], out var intResult2);
                                bodyOb[i] = intResult2.ToString();
                            }
                            if (bodySrc[i] != bodyOb[i])
                            {
                                if (bodySrc[25] != bodyOb[25]) return;
                                ReportDifference(idx.Key, bodySrc[i], bodyOb[i], valueSettings);
                            }
                                
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }
                    finally
                    {
                        Interlocked.Increment(ref _current);
                    }
                });
            });
        }

        private void ReportDifference(string key, string srcVal, string obVal,
            IrfPropSettings.PropertySettings settings)
        {
            _differencesCount.AddOrUpdate(settings.Name,
                new List<string> {key + $@"  LifeCom Value: {srcVal}, DMFI Value: {obVal}"},
                (dicKey, existingVal) =>
                {
                    existingVal.Add(key + $@"  LifeCom Value: {srcVal}, DMFI Value: {obVal}");
                    return existingVal;
                });
        }

        private void ReportMissing(string srcKey)
        {
            _differencesCount.AddOrUpdate("Missing Policies", new List<string> {srcKey},
                (key, existingVal) =>
                {
                    existingVal.Add(srcKey);
                    return existingVal;
                });
        }

        private void LogProcess(string message, bool newLine)
        {
            processTextBox.AppendText(message);
            if (newLine) processTextBox.AppendText(Environment.NewLine);
        }

        private void CreateOutputFile()
        {
            if (!Directory.Exists(Path.GetDirectoryName(Application.ExecutablePath)
                                  + @"\Output\IRF"))
                Directory.CreateDirectory(Path.GetDirectoryName(Application.ExecutablePath)
                                          + @"\Output\IRF");
            using (var file = new StreamWriter(Path.GetDirectoryName(Application.ExecutablePath)
                                               + $@"\Output\IRF\IRF_Diffs_{DateTime.Now:yyyy-dd-M--HH-mm-ss}.txt"))
            {
                foreach (var entry in _differencesCount)
                {
                    file.WriteLine("[{0}]", entry.Key);
                    entry.Value.ForEach(policy => file.WriteLine(policy));
                }
            }
        }


        private void DeleteFiles()
        {
            File.Delete(Directory.GetCurrentDirectory() + @"\Source.txt");
            File.Delete(Directory.GetCurrentDirectory() + @"\Outbound.txt");
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
    }
}
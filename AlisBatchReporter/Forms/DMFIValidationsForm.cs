using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AlisBatchReporter.Classes;

namespace AlisBatchReporter.Forms
{
    public partial class DmfiValidationsForm : Form
    {
        private readonly Dictionary<string, List<string>> _differencesCount = new Dictionary<string, List<string>>();

        public DmfiValidationsForm()
        {
            InitializeComponent();
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

        public async Task<List<IrfRecord>> ReadAllLinesAsync(string path)
        {
            return await ReadAllLinesAsync(path, Encoding.UTF8);
        }

        public async Task<List<IrfRecord>> ReadAllLinesAsync(string path, Encoding encoding)
        {
            var irfs = new List<IrfRecord>();

            // Open the FileStream with the same FileMode, FileAccess
            // and FileShare as a call to File.OpenText would've done.
            using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read, 4096,
                FileOptions.Asynchronous))
            using (var reader = new StreamReader(stream, encoding))
            {
                string line;
                var indexArr = new[]
                {
                    3, 13, 23, 25, 35, 45, 55, 56, 59,
                    69, 74, 94, 96, 97, 122, 137, 152, 162, 172, 182, 192, 202, 212, 213, 215, 217
                };
                while ((line = await reader.ReadLineAsync()) != null)
                {
                    var irf = new IrfRecord()
                    {
                        Key = line.Substring(3, 10)+"-"+line.Substring(45,10),
                        Body = SplitAt(line, indexArr).Select((item, idx) => new IrfRecordProperty
                        {
                            Value = item
                        }).ToList()
                    };
                    for (var i = 0; i < irf.Body.Count -1; i++)
                    {
                        irf.Body[i].Settings = irf.Body[i].GetValue(i);
                    }
                    irfs.Add(irf);
                }                   
            }
            return irfs;
        }

        private async Task ValidateAnalytic(string selectedItem)
        {
            if (selectedItem.Equals("IRF2 File Comparison"))
            {
                DeleteFiles();
                LogProcess("Copy Files Asynch (!)...", false);
                //await CopyFiles();
                var copyTasks = new Task[2];

                var sourceFileName = "LFCM_IRF2.txt";

                var outboundFileName = "IRF2.txt";

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
                var sourceFileRead = ReadAllLinesAsync(Directory.GetCurrentDirectory() + @"\IRF_Source.txt");
                var outboundFileRead = ReadAllLinesAsync(Directory.GetCurrentDirectory() + @"\IRF_Outbound.txt");
                await Task.WhenAll(sourceFileRead, outboundFileRead);
                LogProcess("Done!", true);
                await Compare(await sourceFileRead, await outboundFileRead);
                //Map Files
                //var sourceDic = MapFile(sourceFileRead.Result, Tuple.Create(3, 10), Tuple.Create(45, 11));
                //var outboundDic = MapFile(outboundFileRead.Result, Tuple.Create(3, 10), Tuple.Create(45, 11));
                // Compare
                //var indexArr = new[]
                //{
                //    3, 13, 23, 25, 35, 45, 55, 56, 59,
                //    69, 74, 94, 96, 97, 122, 137, 152, 162, 172, 182, 192, 202, 212, 213, 215, 217, 218
                //};
                LogProcess(@"Validating...", false);
                //SplitAndCompare(sourceDic, outboundDic, indexArr);
                LogProcess(@"Done!", true);
                // Create Output
                processTextBox.AppendText(@"Writing To File...");
                CreateOutputFile();
                processTextBox.AppendText($@"Done!{Environment.NewLine}");
            }

            if (selectedItem.Equals("IRF1 File Comparison"))
            {
                DeleteFiles();
                LogProcess("Copy Files Asynch (!)...", false);
                //await CopyFiles();
                var copyTasks = new Task[2];

                var sourceFileName = "LFCM_IRF1.txt";

                var outboundFileName = "IRF1.txt";

                var envPath = @"\\dmfdwh001ut\E";

                copyTasks[0] = CopyFileAsync(
                    $@"{envPath}\Deploy\Prod\FTP\Validation\ARCVAL\{sourceFileName}",
                    Path.Combine(Directory.GetCurrentDirectory(), "IRF_Source.txt"));

                copyTasks[1] = CopyFileAsync(
                    $@"{envPath}\Deploy\Prod\FTP\Outbound\ARCVAL\{outboundFileName}",
                    Path.Combine(Directory.GetCurrentDirectory(), "IRF_Outbound.txt"));

                await Task.WhenAll(copyTasks);
                LogProcess("Done!", true);
                //Read New Files
                LogProcess("Reading Files Asynch (!)...", false);
                var sourceFileRead = ReadAllLinesAsync(Directory.GetCurrentDirectory() + @"\IRF_Source.txt");
                var outboundFileRead = ReadAllLinesAsync(Directory.GetCurrentDirectory() + @"\IRF_Outbound.txt");
                //await Task.WhenAll(sourceFileRead, outboundFileRead);
                LogProcess("Done!", true);
                await Compare(await sourceFileRead, await outboundFileRead);
                //Map Files
                //var test = outboundFileRead.Result.Where(ob => sourceFileRead.Result.Contains(ob.Substring(3, 10)));
                //var sourceDic = MapFile(sourceFileRead.Result, Tuple.Create(3, 10));
                //var outboundDic = MapFile(outboundFileRead.Result, Tuple.Create(3, 10));
                // Compare
                //var indexArr = new[]
                //{
                //    3, 13, 23, 33, 35, 36, 38, 48, 58
                //};
                //processTextBox.AppendText(@"Validating...");
                //SplitAndCompare(sourceDic, outboundDic, indexArr);
                //processTextBox.AppendText($@"Done!{Environment.NewLine}");
                // Create Output
                processTextBox.AppendText(@"Writing To File...");
                CreateOutputFile();
                processTextBox.AppendText($@"Done!{Environment.NewLine}");
            }
        }

        private async Task Compare(List<IrfRecord> sourceFileRead, List<IrfRecord> outboundFileRead)
        {
            foreach (var src in sourceFileRead)
            {
                var matched = outboundFileRead.FirstOrDefault(ob => ob.Key == src.Key);
                if (matched == null)
                {
                    ReportMissing(src.Key);
                    continue;
                }
                await Task.Run(() => CompareEntities(src, matched));
            }
        }

        private void CompareEntities(IrfRecord src, IrfRecord matched)
        {
            for (var i = 0; i < src.Body.Count - 1; i++)
            {
                if (src.Body[i].Settings.ToIgnore) continue;
                if (src.Body[i].Settings.Intable)
                {
                    int.TryParse(src.Body[i].Value, out var intResult);
                    src.Body[i].Value = intResult.ToString();
                    int.TryParse(matched.Body[i].Value, out var intResult2);
                    matched.Body[i].Value = intResult2.ToString();
                }
                if (src.Body[i].Value != matched.Body[i].Value)
                {
                    if (src.Body[i].Settings.Name.Equals("Payment Mode")) continue;
                    ReportDiff(src.Body[i], matched.Body[i], src.Key);
                }
            }
        }

        private void ReportMissing(string srcKey)
        {
            if (_differencesCount.ContainsKey("Missing Policies"))
                _differencesCount["Missing Policies"].Add(srcKey);
            else
                try
                {
                    _differencesCount.Add("Missing Policies", new List<string>
                    {
                        srcKey
                    });
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
        }

        private void ReportDiff(IrfRecordProperty srcProp, IrfRecordProperty obProp, string srcKey)
        {
            if (_differencesCount.ContainsKey(srcProp.Settings.Name))
                _differencesCount[srcProp.Settings.Name].Add(srcKey + $@"  LifeCom Value: {srcProp.Value}, DMFI Value: {obProp.Value}");
            else
                try
                {
                    _differencesCount.Add(srcProp.Settings.Name, new List<string>
                    {
                        srcKey + $@"  LifeCom Value: {srcProp.Value}, DMFI Value: {obProp.Value}"
                    });
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
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

        private void SplitAndCompare(Dictionary<string, string> sourceDic, Dictionary<string, string> outboundDic,
            int[] indexCuts)
        {
            foreach (var key in sourceDic.Keys)
                if (outboundDic.TryGetValue(key, out var outboundValue))
                {
                    if (!sourceDic.TryGetValue(key, out var sourceValue)) continue;
                    var sourceSplittedRow = SplitAt(sourceValue, indexCuts).ToList();
                    var outboundSplittedRow = SplitAt(outboundValue, indexCuts).ToList();
                    CompareRows(sourceSplittedRow, outboundSplittedRow);
                }
        }

        private Dictionary<string, string> MapFile(string[] fileRows, params Tuple<int, int>[] keyIndexes)
        {
            var dic = new Dictionary<string, string>();
            foreach (var row in fileRows)
            {
                var key = "";
                foreach (var pair in keyIndexes)
                    key += row.Substring(pair.Item1, pair.Item2);
                dic[key] = row;
            }
            return dic;
        }

        private void DeleteFiles()
        {
            File.Delete(Directory.GetCurrentDirectory() + @"\Source.txt");
            File.Delete(Directory.GetCurrentDirectory() + @"\Outbound.txt");
        }

        private void CompareRows(List<string> source, List<string> outbound)
        {
            for (var i = 0; i < source.Count - 1; i++)
                if (!source[i].Equals(outbound[i]))
                    Compare(source, outbound, i);
        }

        private void Compare(List<string> source, List<string> outbound, int idx)
        {
            if (validationTypesCombobox.Text.Equals("IRF1 File Comparison"))
            {
                var irf1Value = new Irf1Values();
                var entity = irf1Value.GetValue(idx);
                var idxName = entity.Name;
                if (!source[entity.IdxValue].Equals(outbound[entity.IdxValue]))
                    Task.Run(() =>
                        AddDiffrence(idxName, source[1], source[entity.IdxValue], outbound[entity.IdxValue]));
            }

            if (validationTypesCombobox.Text.Equals("IRF2 File Comparison"))
            {
                var irf2Value = new Irf2Values();
                var entity = irf2Value.GetValue(idx);
                string irf2ValueName;
                if (entity != null && !entity.Ignore)
                {
                    irf2ValueName = entity.Name;
                    var sourceVal = source[entity.IdxValue];
                    var outboundVal = outbound[entity.IdxValue];
                    if (new[] { "24201", "30109", "30110" }.Contains(source[10])) return;
                    if (entity.Intable)
                    {
                        if (double.TryParse(sourceVal, out var castedToDoubleSource) &&
                            double.TryParse(outboundVal, out var castedToDoubleOutbound))
                        {
                            var diff = Math.Abs(castedToDoubleSource - castedToDoubleOutbound);
                            if (!irf2ValueName.Equals("Modal Premium") && diff > 0 ||
                                irf2ValueName.Equals("Modal Premium") && diff > 2)
                                Task.Run(() => AddDiffrence(
                                    irf2ValueName,
                                    source[1] + " - " + source[6],
                                    castedToDoubleSource.ToString(CultureInfo.InvariantCulture),
                                    castedToDoubleOutbound.ToString(CultureInfo.InvariantCulture)));
                        }
                    }
                    else
                    {
                        if (!source[24].Equals("10")) return;
                        if (!sourceVal.Equals(outboundVal))
                        {
                            if (irf2ValueName.Equals("Payment Mode")) return;
                            Task.Run(() => AddDiffrence(
                                irf2ValueName,
                                source[1] + "-" + source[6],
                                sourceVal,
                                outboundVal));
                        }
                    }
                }
            }
        }

        private void AddDiffrence(object idxName, string polNo, string souceValue, string outboundValue)
        {
            if (idxName.Equals("Payment Mode")) return;
            if (_differencesCount.ContainsKey(idxName.ToString()))
                _differencesCount[idxName.ToString()]
                    .Add(polNo + $@"  LifeCom Value: {souceValue}, DMFI Value: {outboundValue}");
            else
                _differencesCount.Add(idxName.ToString(), new List<string>
                {
                    polNo + $@"  LifeCom Value: {souceValue}, DMFI Value: {outboundValue}"
                });
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
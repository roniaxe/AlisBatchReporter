using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using AlisBatchReporter.Classes;

namespace AlisBatchReporter.Forms
{
    public partial class DmfiValidationsForm : Form
    {
        private readonly Dictionary<string, List<string>> _differencesCount = new Dictionary<string, List<string>>();
        private readonly BackgroundWorker _mCopier;

        private readonly ProgressChanged _onChange;
        private readonly CopyError _onError;

        private readonly string[] _pathList =
        {
            @"\\dmfdwh001ut\E\Deploy\Prod\FTP\validation\AnalyticFeed\",
            @"\\dmfdwh001ut\E\Deploy\Prod\FTP\Outbound\AnalyticFeed\"
        };

        private string[] _fileList;

        public DmfiValidationsForm()
        {
            InitializeComponent();
            _mCopier = new BackgroundWorker();
            _mCopier.DoWork += Copier_DoWork;
            _mCopier.RunWorkerCompleted += Copier_RunWorkerCompleted;
            _mCopier.WorkerSupportsCancellation = true;
            _onChange += Copier_ProgressChanged;
            _onError += Copier_Error;
            ChangeUi(false);
        }

        private void validateButton_Click(object sender, EventArgs e)
        {
            ValidateAnalytic(validationTypesCombobox.Text);
        }

        private void Copier_DoWork(object sender, DoWorkEventArgs e)
        {
            // Create list of files to copy
            var theExtensions = _fileList;
            var files = new List<FileInfo>();
            //string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            long maxbytes = 0;
            foreach (var ext in theExtensions)
            foreach (var path in _pathList)
            {
                var dir = new DirectoryInfo(path);
                var folder = dir.GetFiles(ext, SearchOption.AllDirectories);
                foreach (var file in folder)
                {
                    if ((file.Attributes & FileAttributes.Directory) != 0) continue;
                    files.Add(file);
                    maxbytes += file.Length;
                }
            }
            // Copy files
            long bytes = 0;
            foreach (var file in files)
            {
                try
                {
                    string fileName;
                    if (file.Name.Equals("LFCM_IRF2.txt") || file.Name.Equals("LFCM_IRF1.txt"))
                        fileName = "Source.txt";
                    else
                        fileName = "Outbound.txt";
                    BeginInvoke(_onChange, new UiProgress(file.Name, bytes, maxbytes));
                    File.Copy(file.FullName, Path.Combine(Directory.GetCurrentDirectory(), fileName), true);
                }
                catch (Exception ex)
                {
                    var err = new UiError(ex, file.FullName);
                    Invoke(_onError, err);
                    if (err.Result == DialogResult.Cancel) break;
                }
                bytes += file.Length;
            }
        }

        private void Copier_ProgressChanged(UiProgress info)
        {
            // Update progress
            progressBar1.Value = (int) (100.0 * info.Bytes / info.Maxbytes);
            label1.Text = @"Copying " + info.Name;
        }

        private void Copier_Error(UiError err)
        {
            // Error handler
            var msg =
                $"Error copying file {err.Path} {Environment.NewLine} {err.Msg}{Environment.NewLine}Click OK to continue copying files";
            err.Result = MessageBox.Show(msg, @"Copy error", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
        }

        private void Copier_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // Operation completed, update UI
            ChangeUi(false);
            validateButton.Text = @"Validate";
        }

        private void ChangeUi(bool docopy)
        {
            label1.Visible = docopy;
            progressBar1.Visible = docopy;
            validateButton.Text = docopy ? "Cancel" : "Copy";
            label1.Text = @"Starting copy...";
            progressBar1.Value = 0;
        }

        private void ValidateAnalytic(string selectedItem)
        {
            var docopy = validateButton.Text == @"Copy";

            if (selectedItem.Equals("IRF2 File Comparison"))
            {
                DeleteFiles();
                ChangeUi(docopy);
                if (docopy)
                {
                    _fileList = new[]
                    {
                        @"LFCM_IRF2.txt",
                        @"IRF2.txt"
                    };
                    _mCopier.RunWorkerAsync();
                    while (_mCopier.IsBusy)
                        Application.DoEvents();
                    //Read New Files
                    var sourceFileRead = File.ReadAllLines(Directory.GetCurrentDirectory() + @"\Source.txt");
                    var outboundFileRead = File.ReadAllLines(Directory.GetCurrentDirectory() + @"\Outbound.txt");
                    //Map Files
                    var sourceDic = MapFile(sourceFileRead, Tuple.Create(3, 10), Tuple.Create(45, 11));
                    var outboundDic = MapFile(outboundFileRead, Tuple.Create(3, 10), Tuple.Create(45, 11));
                    // Compare
                    var indexArr = new[]
                    {
                        3, 13, 23, 25, 35, 45, 55, 56, 59,
                        69, 74, 94, 96, 97, 122, 137, 152, 162, 172, 182, 192, 202, 212, 213, 215, 217, 218
                    };
                    processTextBox.AppendText(@"Validating...");
                    SplitAndCompare(sourceDic, outboundDic, indexArr);
                    processTextBox.AppendText($@"Done!{Environment.NewLine}");
                    // Create Output
                    processTextBox.AppendText(@"Writing To File...");
                    CreateOutputFile();
                    processTextBox.AppendText($@"Done!{Environment.NewLine}");
                }
                else
                {
                    _mCopier.CancelAsync();
                }
            }

            if (selectedItem.Equals("IRF1 File Comparison"))
            {
                DeleteFiles();
                ChangeUi(docopy);
                if (docopy)
                {
                    _fileList = new[]
                    {
                        @"LFCM_IRF1.txt",
                        @"IRF1.txt"
                    };
                    _mCopier.RunWorkerAsync();
                    while (_mCopier.IsBusy)
                        Application.DoEvents();
                    //Read New Files
                    var sourceFileRead = File.ReadAllLines(Directory.GetCurrentDirectory() + @"\Source.txt");
                    var outboundFileRead = File.ReadAllLines(Directory.GetCurrentDirectory() + @"\Outbound.txt");
                    //Map Files
                    var sourceDic = MapFile(sourceFileRead, Tuple.Create(3, 10));
                    var outboundDic = MapFile(outboundFileRead, Tuple.Create(3, 10));
                    // Compare
                    var indexArr = new[]
                    {
                        3, 13, 23, 33, 35, 36, 38, 48, 58
                    };
                    processTextBox.AppendText(@"Validating...");
                    SplitAndCompare(sourceDic, outboundDic, indexArr);
                    processTextBox.AppendText($@"Done!{Environment.NewLine}");
                    // Create Output
                    processTextBox.AppendText(@"Writing To File...");
                    CreateOutputFile();
                    processTextBox.AppendText($@"Done!{Environment.NewLine}");
                }
            }
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
            {
                if (outboundDic.TryGetValue(key, out var outboundValue))
                {
                    if (!sourceDic.TryGetValue(key, out var sourceValue)) continue;
                    var splittedSourceRow = SplitAt(sourceValue, indexCuts)
                        .ToList();
                    var splittedOutboundRow = SplitAt(outboundValue, indexCuts)
                        .ToList();
                    CompareRows(splittedSourceRow, splittedOutboundRow);
                }
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

            //File.Copy(Path.Combine(@"\\dmfdwh001pr\X\Deploy\Prod\FTP\Outbound\AnalyticFeed\", outboundFile),
            //    Path.Combine(Directory.GetCurrentDirectory(), "Outbound.txt"), true);
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
                    Task.Run(() => AddDiffrence(idxName, source[1], source[entity.IdxValue], outbound[entity.IdxValue]));
            }

            if (validationTypesCombobox.Text.Equals("IRF2 File Comparison"))
            {
                var irf2Value = new Irf2Values();
                var entitiy = irf2Value.GetValue(idx);
                string irf2ValueName;
                if (entitiy != null && !entitiy.Ignore)
                {
                    irf2ValueName = entitiy.Name;
                    if (entitiy.Intable)
                    {
                        if (double.TryParse(source[entitiy.IdxValue], out var castedToDoubleSource) &&
                            double.TryParse(outbound[entitiy.IdxValue], out var castedToDoubleOutbound))
                        {
                            var diff = Math.Abs(castedToDoubleSource - castedToDoubleOutbound);
                            if (!irf2ValueName.Equals("Modal Premium") && diff > 0 ||
                                irf2ValueName.Equals("Modal Premium") && diff > 2)
                                Task.Run(() => AddDiffrence(irf2ValueName, source[1] + " - " + source[6],
                                    castedToDoubleSource.ToString(CultureInfo.InvariantCulture),
                                    castedToDoubleOutbound.ToString(CultureInfo.InvariantCulture)));
                        }
                    }
                    else
                    {
                        if (!source[entitiy.IdxValue].Equals(outbound[entitiy.IdxValue]))
                            Task.Run(() => AddDiffrence(irf2ValueName,
                                source[1] + "-" + source[6],
                                source[entitiy.IdxValue],
                                outbound[entitiy.IdxValue]));
                    }
                }
            }
        }

        private void AddDiffrence(object idxName, string polNo, string souceValue, string outboundValue)
        {
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

        private delegate void ProgressChanged(UiProgress info);

        private delegate void CopyError(UiError err);
    }
}
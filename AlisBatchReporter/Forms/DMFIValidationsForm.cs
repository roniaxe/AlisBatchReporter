using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace AlisBatchReporter.Forms
{
    public partial class DmfiValidationsForm : Form
    {
        readonly Dictionary<string, List<string>> _differencesCount = new Dictionary<string, List<string>>();

        public DmfiValidationsForm()
        {
            InitializeComponent();
        }

        private void validateButton_Click(object sender, EventArgs e)
        {
            ValidateAnalytic(validationTypesCombobox.Text);
        }

        private void ValidateAnalytic(string selectedItem)
        {
            if (selectedItem.Equals("IRF2 File Comparison"))
            {
                DeleteAndCopyFiles(@"LFCM_IRF2.txt", @"IRF2.txt");
                //Read New Files
                var sourceFileRead = File.ReadAllLines(Directory.GetCurrentDirectory() + @"\Source.txt");
                var outboundFileRead = File.ReadAllLines(Directory.GetCurrentDirectory() + @"\Outbound.txt");
                //Map Files
                var sourceDic = MapFile(sourceFileRead, Tuple.Create(3, 10), Tuple.Create(45, 11));
                var outboundDic = MapFile(outboundFileRead, Tuple.Create(3, 10), Tuple.Create(45, 11));
                // Compare
                var indexArr = new int[]
                {
                    3, 13, 23, 25, 35, 45, 55, 56, 59,
                    69, 74, 94, 96, 97, 122, 137, 152, 162, 172, 182, 192, 202, 212, 213, 215, 217, 218
                };
                SplitAndCompare(sourceDic, outboundDic, indexArr);
                // Create Output
                CreateOutputFile();
            }

            if (selectedItem.Equals("IRF1 File Comparison"))
            {
                DeleteAndCopyFiles(@"LFCM_IRF1.txt", @"IRF1.txt");
                //Read New Files
                var sourceFileRead = File.ReadAllLines(Directory.GetCurrentDirectory() + @"\Source.txt");
                var outboundFileRead = File.ReadAllLines(Directory.GetCurrentDirectory() + @"\Outbound.txt");
                //Map Files
                var sourceDic = MapFile(sourceFileRead, Tuple.Create(3, 10));
                var outboundDic = MapFile(outboundFileRead, Tuple.Create(3, 10));
                // Compare
                var indexArr = new int[]
                {
                    3, 13, 23, 33, 35, 36, 38, 48, 58
                };
                SplitAndCompare(sourceDic, outboundDic, indexArr);
                // Create Output
                CreateOutputFile();
            }
        }

        private void CreateOutputFile()
        {
            using (StreamWriter file = new StreamWriter("myfile.txt"))
                foreach (var entry in _differencesCount)
                {
                    if (!entry.Key.Equals(IRF2.PlanCode.ToString()) && !entry.Key.Equals(IRF2.PlanName.ToString()))
                    {
                        file.WriteLine("[{0}]", entry.Key);
                        entry.Value.ForEach(policy => file.WriteLine(policy));
                    }
                }
        }

        private void SplitAndCompare(Dictionary<string, string> sourceDic, Dictionary<string, string> outboundDic,
            int[] indexCuts)
        {
            foreach (string key in sourceDic.Keys)
            {
                string outboundValue;
                if (outboundDic.TryGetValue(key, out outboundValue))
                {
                    string sourceValue;
                    if (sourceDic.TryGetValue(key, out sourceValue))
                    {
                        var splittedSourceRow = SplitAt(sourceValue, indexCuts)
                            .ToList();
                        var splittedOutboundRow = SplitAt(outboundValue, indexCuts)
                            .ToList();
                        CompareRows(splittedSourceRow, splittedOutboundRow);
                    }
                }
            }
        }

        private Dictionary<string, string> MapFile(string[] fileRows, params int[] keyIndexes)
        {
            var dic = new Dictionary<string, string>();
            foreach (var row in fileRows)
            {
                string key = "";
                for (int i = 0; i < keyIndexes.Length; i += 2)
                {
                    key += row.Substring(keyIndexes[i], keyIndexes[i + 1]);
                }
                dic[key] = row;
            }
            return dic;
        }

        private Dictionary<string, string> MapFile(string[] fileRows, params Tuple<int, int>[] keyIndexes)
        {
            var dic = new Dictionary<string, string>();
            foreach (var row in fileRows)
            {
                string key = "";
                foreach (var pair in keyIndexes)
                {
                    key += row.Substring(pair.Item1, pair.Item2);
                }
                dic[key] = row;
            }
            return dic;
        }

        private void DeleteAndCopyFiles(string sourcFile, string outboundFile)
        {
            File.Delete(Directory.GetCurrentDirectory() + @"\Source.txt");
            File.Delete(Directory.GetCurrentDirectory() + @"\Outbound.txt");

            // Copy File Source
            File.Copy(Path.Combine(@"\\dmfdwh001pr\X\Deploy\Prod\FTP\validation\AnalyticFeed\", sourcFile),
                Path.Combine(Directory.GetCurrentDirectory(), "Source.txt"), true);
            // Copy File Outbound
            File.Copy(Path.Combine(@"\\dmfdwh001pr\X\Deploy\Prod\FTP\Outbound\AnalyticFeed\", outboundFile),
                Path.Combine(Directory.GetCurrentDirectory(), "Outbound.txt"), true);
        }

        private void CompareRows(List<string> source, List<string> outbound)
        {
            for (int i = 0; i < source.Count - 1; i++)
            {
                if (!source[i].Equals(outbound[i]))
                {
                    Compare(source, outbound, i);
                }
            }
        }

        private void Compare(List<string> source, List<string> outbound, int idx)
        {
            if (validationTypesCombobox.Text.Equals("IRF1 File Comparison"))
            {
                var idxName = (IRF1) idx;
                if (!source[idx].Equals(outbound[idx]))
                {
                    AddDiffrence(idxName, source[1]);
                }
            }

            if (validationTypesCombobox.Text.Equals("IRF2 File Comparison"))
            {
                var idxName = (IRF2)idx;
                if (idx > 18 && idx < 23)
                {
                    double castedToDoubleSource, castedToDoubleOutbound;
                    if (Double.TryParse(source[idx], out castedToDoubleSource) &&
                        Double.TryParse(outbound[idx], out castedToDoubleOutbound))
                    {
                        if (Math.Abs(castedToDoubleSource - castedToDoubleOutbound) > 0)
                        {
                            AddDiffrence(idxName, source[1] + " - " + source[6]);
                        }
                    }
                }
                else
                {
                    if (!source[idx].Equals(outbound[idx]))
                    {
                        AddDiffrence(idxName, source[1] + "-" + source[6]);
                    }
                }
            }
            
        }

        private void AddDiffrence(object idxName, string polNo)
        {
            if (_differencesCount.ContainsKey(idxName.ToString()))
            {
                _differencesCount[idxName.ToString()].Add(polNo);
            }
            else
            {
                _differencesCount.Add(idxName.ToString(), new List<string>
                {
                    polNo
                });
            }
        }

        public string[] SplitAt(string source, params int[] index)
        {
            index = index.Distinct().OrderBy(x => x).ToArray();
            string[] output = new string[index.Length + 1];
            int pos = 0;

            for (int i = 0; i < index.Length; pos = index[i++])
                output[i] = source.Substring(pos, index[i] - pos);

            output[index.Length] = source.Substring(pos);
            return output;
        }
    }
    public enum IRF1
    {
        CompanyCd = 0,
        PolNo = 1,
        WritingAgency = 2,
        WritingAgent = 3,
        LOB = 4,
        ActiveFlag = 5,
        PolicyStatus = 6,
        MembershipNo = 7,
        PolicyIssueDate = 8
    }

    public enum IRF2
    {
        CompanyCd = 0,
        PolNo = 1,
        MembershipNo = 2,
        LOB = 3,
        Region = 4,
        WritingAgency = 5,
        WritingAgent = 6,
        PrimaryAgentFlag = 7,
        Commission = 8,
        Servicing = 9,
        PlanCode = 10,
        PlanName = 11,
        IssuedAge = 12,
        IssuedGender = 13,
        LastName = 14,
        FirstName = 15,
        MiddleName = 16,
        PolicyIssueDate = 17,
        PaidToPaid = 18,
        CurrentCashValue = 19,
        ModalPremium = 20,
        FaceValue = 21,
        SpouseFaceValue = 22,
        ActiveFlag = 23,
        PolicyStatus = 24,
        PaymentMode = 25,
        PaymentForm = 26
    }
}
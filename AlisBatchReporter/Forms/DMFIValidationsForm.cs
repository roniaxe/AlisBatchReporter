using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace AlisBatchReporter.Forms
{
    public partial class DmfiValidationsForm : Form
    {
        readonly Dictionary<string, int> _differencesCound = new Dictionary<string, int>();
        public DmfiValidationsForm()
        {
            InitializeComponent();
        }

        private void validateButton_Click(object sender, EventArgs e)
        {
            if (validationTypesCombobox.Text.Equals("AnalyticFeed"))
            {
                ValidateAnalytic();
            }
        }

        private void ValidateAnalytic()
        {
            File.Delete(Directory.GetCurrentDirectory() + @"\SourceIRF2.txt");
            File.Delete(Directory.GetCurrentDirectory() + @"\OutboundIRF2.txt");

            progressBar1.Visible = true;
            // Copy File Source
            File.Copy(Path.Combine(@"\\dmfdwh001pr\X\Deploy\Prod\FTP\validation\AnalyticFeed\", @"LFCM_IRF2.txt"),
                Path.Combine(Directory.GetCurrentDirectory(), "SourceIRF2.txt"), true);
            // Copy File Outbound
            File.Copy(Path.Combine(@"\\dmfdwh001pr\X\Deploy\Prod\FTP\Outbound\AnalyticFeed\", @"IRF2.txt"),
                Path.Combine(Directory.GetCurrentDirectory(), "OutboundIRF2.txt"), true);

            var sourceFileRead = File.ReadAllLines(Directory.GetCurrentDirectory() + @"\SourceIRF2.txt");
            var outboundFileRead = File.ReadAllLines(Directory.GetCurrentDirectory() + @"\OutboundIRF2.txt");

            var sourceDic = new Dictionary<string, string>();
            var outboundDic = new Dictionary<string, string>();
            foreach (var row in sourceFileRead)
            {
                var key = row.Substring(3, 10) + row.Substring(45, 11);
                sourceDic[key] = row;
            }
            foreach (var row in outboundFileRead)
            {
                var key = row.Substring(3, 10) + row.Substring(45, 11);
                outboundDic[key] = row;
            }

            foreach (string key in sourceDic.Keys)
            {
                string outboundValue;
                if (outboundDic.TryGetValue(key, out outboundValue))
                {
                    string sourceValue;
                    if (sourceDic.TryGetValue(key, out sourceValue))
                    {
                        var splittedSourceRow = SplitAt(sourceValue, 3, 13, 23, 25, 35, 45, 55, 56, 59,
                                69, 74, 94, 96, 97, 122, 137, 152, 162, 172, 182, 192, 202, 212, 213, 215, 217, 218)
                            .ToList();
                        var splittedOutboundRow = SplitAt(outboundValue, 3, 13, 23, 25, 35, 45, 55, 56, 59,
                                69, 74, 94, 96, 97, 122, 137, 152, 162, 172, 182, 192, 202, 212, 213, 215, 217, 218)
                            .ToList();
                        CompareRows(splittedSourceRow, splittedOutboundRow);
                    }
                }
            }

            using (StreamWriter file = new StreamWriter("myfile.txt"))
                foreach (var entry in _differencesCound)
                    file.WriteLine("[{0} {1}]", entry.Key, entry.Value);
        }

        private void CompareRows(List<string> source, List<string> outbound)
        {
            for (int i = 0; i < 27; i++)
            {
                if (!source[i].Equals(outbound[i]))
                {
                    Compare(source, outbound, i);
                }
            }
        }

        private void Compare(List<string> source, List<string> outbound, int idx)
        {
            IRF2 idxName = (IRF2)idx;
            
            if (idx > 18 && idx < 23)
            {
                double castedToDoubleSource, castedToDoubleOutbound;
                if (Double.TryParse(source[idx], out castedToDoubleSource) &&
                    Double.TryParse(outbound[idx], out castedToDoubleOutbound))
                {
                    if (Math.Abs(castedToDoubleSource - castedToDoubleOutbound) > 0)
                    {
                        AddDiffrence(idxName);
                    }
                }
            }
            else
            {
                if (!source[idx].Equals(outbound[idx]))
                {
                    AddDiffrence(idxName);
                }
            }
        }

        private void AddDiffrence(IRF2 idxName)
        {
            if (_differencesCound.ContainsKey(idxName.ToString()))
            {
                _differencesCound[idxName.ToString()]++;
            }
            else
            {
                _differencesCound.Add(idxName.ToString(), 1);
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
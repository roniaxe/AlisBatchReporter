using System;
using System.Windows.Forms;

namespace AlisBatchReporter.Views
{
    public interface IArcvalView
    {
        event Action Compared;

        event Action OverrideFilesChecked;

        Panel OverridePanel { get; }

        string ProcessTextBox { get; set; }

        bool CopyFiles { get; }

        string SourceFileName { get; set; }

        string OutboundFileName { get; set; }

        bool ProdRadioButton { get; }

        bool UatRadioButton { get; }

        bool OverrideFiles { get; }

        void LogProcess(string text, bool newLine);
    }
}

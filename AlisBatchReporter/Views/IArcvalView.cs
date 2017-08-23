using System;
using System.Windows.Forms;

namespace AlisBatchReporter.Views
{
    public interface IArcvalView
    {
        event Action Compared;

        TextBox ProcessTextBox { get; }

        bool CopyFiles { get; }

        void LogProcess(string text, bool newLine);
    }
}

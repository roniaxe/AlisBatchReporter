using System;

namespace AlisBatchReporter.Views
{
    public interface IArcvalView
    {
        event Action Compared;

        string ProcessTextBox { get; }

        bool CopyFiles { get; }

        void LogProcess(string text, bool newLine);
    }
}

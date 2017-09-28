using System;

namespace AlisBatchReporter.Views
{
    public interface ILexisNexisView
    {
        event Action NexisLexisValidated;
        string ProcessText { get; }
        void LogProcess(string logText, bool newLine);
    }
}
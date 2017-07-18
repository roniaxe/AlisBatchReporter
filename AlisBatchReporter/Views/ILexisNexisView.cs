using System;

namespace AlisBatchReporter.Views
{
    public interface ILexisNexisView
    {
        event Action NexisLexisValidated;
        event Action NexisLexisClosed;
        string ProcessText { get; }
        void LogProcess(string logText, bool newLine);
    }
}
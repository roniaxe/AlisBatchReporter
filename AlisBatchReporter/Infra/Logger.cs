using System;
using System.IO;

namespace AlisBatchReporter.Infra
{
    public static class Logger
    {
        private static StreamWriter _swLog;
        private const string SLogFilePath = "log.txt";

        static Logger()
        {
            OpenLogger();
        }

        public static void OpenLogger()
        {
            _swLog = new StreamWriter(SLogFilePath, false) {AutoFlush = true};
        }

        public static void LogThisLine(string sLogLine)
        {
            _swLog.WriteLine(DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString() + "\t:" + "\t" + sLogLine);
            _swLog.Flush();
        }

        public static void CloseLogger()
        {
            _swLog.Flush();
            _swLog.Close();
        }
    }
}

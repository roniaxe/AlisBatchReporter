using System;
using System.Threading;
using System.Windows.Forms;
using AlisBatchReporter.Forms;
using AlisBatchReporter.Infra;

namespace AlisBatchReporter
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += ApplicationOnThreadException;
            AppDomain.CurrentDomain.UnhandledException += AppDomainUnhandledException;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }

        private static void AppDomainUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var message = $@"Sorry, Something went wrong:{Environment.NewLine}{((Exception)e.ExceptionObject).Message}";
            Console.WriteLine($@"Error {DateTimeOffset.Now}: {e.ExceptionObject}");
            Logger.OpenLogger();
            Logger.LogThisLine(e.ExceptionObject.ToString());
            Logger.CloseLogger();
            MessageBox.Show(message, @"Unexpected Error");
        }

        private static void ApplicationOnThreadException(object sender, ThreadExceptionEventArgs e)
        {
            var message = $@"Sorry, Something went wrong:{Environment.NewLine}{e.Exception.Message}";
            Console.WriteLine($@"Error {DateTimeOffset.Now}: {e.Exception}");
            Logger.OpenLogger();
            Logger.LogThisLine(e.Exception.Message);
            Logger.LogThisLine(e.Exception.StackTrace);
            Logger.LogThisLine(e.Exception.InnerException?.Message);
            Logger.CloseLogger();
            MessageBox.Show(message, @"Unexpected Error");
        }
    }
}

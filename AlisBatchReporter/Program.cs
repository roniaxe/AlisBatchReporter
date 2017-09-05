using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlServerCe;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using AlisBatchReporter.Forms;
using AlisBatchReporter.Models;

namespace AlisBatchReporter
{
    internal static class Program
    {
        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += ApplicationOnThreadException;
            AppDomain.CurrentDomain.UnhandledException += AppDomainUnhandledException;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //CreateDbIfNotExists();
            Application.Run(new MainForm(new AlisDbContext("CompactDBContext")));
            
        }

        private static void AppDomainUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var message =
                $@"Sorry, Something went wrong:{Environment.NewLine}{((Exception) e.ExceptionObject).Message}";
            Console.WriteLine($@"Error {DateTimeOffset.Now}: {e.ExceptionObject}");
            MessageBox.Show(message, @"Unexpected Error");
        }

        private static void ApplicationOnThreadException(object sender, ThreadExceptionEventArgs e)
        {
            var message = $@"Sorry, Something went wrong:{Environment.NewLine}{e.Exception.Message}";
            Console.WriteLine($@"Error {DateTimeOffset.Now}: {e.Exception}");
            MessageBox.Show(message, @"Unexpected Error");
        }
    }
}
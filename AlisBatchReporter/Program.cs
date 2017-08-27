using System;
using System.Data.SqlServerCe;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using AlisBatchReporter.Forms;

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
            CreateDbIfNotExists();
            Application.Run(new MainForm());
        }

        private static void CreateDbIfNotExists()
        {
            if (!File.Exists("alisReporter.sdf"))
            {
                var engine = new SqlCeEngine("DataSource=\"alisReporter.sdf\"; Password=\"12345\";");
                engine.CreateDatabase();
                using (SqlCeConnection conn = new SqlCeConnection("DataSource=\"alisReporter.sdf\"; Password=\"12345\";"))
                {
                    conn.Open();
                    SqlCeCommand command = new SqlCeCommand(
                        @"CREATE TABLE saved_credentials (ID int primary key Identity(1, 1), 
                                                            USERNAME nvarchar(100), 
                                                            PASSWORD nvarchar(1000), 
                                                            HOST nvarchar(100), 
                                                            DB nvarchar(100), 
                                                            NAME nvarchar(100), 
                                                            CONN_STRING nvarchar(1000),
                                                            CHOSE_LAST nvarchar(10),
                                                            SAVED nvarchar(10))", conn);
                    command.ExecuteNonQuery();
                    command = new SqlCeCommand(
                        "INSERT INTO saved_credentials (HOST, NAME) Values ('10.134.5.30', 'Prod')"
                        , conn);
                    command.ExecuteNonQuery();
                    command = new SqlCeCommand(
                        "INSERT INTO saved_credentials (HOST, NAME) Values ('10.134.8.10', 'Uat')"
                        , conn);
                    command.ExecuteNonQuery();
                    command = new SqlCeCommand(
                        "INSERT INTO saved_credentials (HOST, NAME) Values ('876630-sqldev.fblife.com', 'White/Red/Blue')"
                        , conn);
                    command.ExecuteNonQuery();
                    command = new SqlCeCommand(
                        "INSERT INTO saved_credentials (HOST, NAME) Values ('alis-db-sql3', 'Sapiens')"
                        , conn);
                    command.ExecuteNonQuery();
                }
            }
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
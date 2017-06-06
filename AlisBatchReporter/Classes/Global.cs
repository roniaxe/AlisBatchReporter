using System;

namespace AlisBatchReporter.Classes
{
    static class Global
    {
        public static string Env { get; set; } = Properties.Settings.Default.LastSaveEnv;

        public static string ChosenConnection { get; set; } = Properties.Settings.Default.LastSaveConnStr;
        public static string Db { get; set; }
        public static bool SavedCheckBox { get; set; }
        public static Version Version { get; set; } = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
    }
}
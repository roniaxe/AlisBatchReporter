using System;
using AlisBatchReporter.Models;

namespace AlisBatchReporter.Classes
{
    internal static class Global
    {
        public static SavedCredentials SavedCredentials { get; set; }
        public static Version Version { get; set; } = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;

        public static void PropSetter(SavedCredentials sc) => SavedCredentials = sc;
    }
}
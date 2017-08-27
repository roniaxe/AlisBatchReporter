using System;

namespace AlisBatchReporter.Classes
{
    internal static class Global
    {
        public static int Id { get; set; }
        public static string Username { get; set; }
        public static string Password { get; set; }
        public static string Host { get; set; }
        public static string Db { get; set; }
        public static string Name { get; set; } 
        public static string ConnString { get; set; } 
        public static string ChoseLast { get; set; }       
        public static string SavedCheckBox { get; set; }             
        public static Version Version { get; set; } = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;

        public static void PropSetter(
            int id,
            string username, 
            string password, 
            string host, 
            string db, 
            string name, 
            string connString,
            string choseLast,
            string saved)
        {
            Id = id;
            Username = username;
            Password = password;
            Host = host;
            Db = db;
            Name = name;
            ConnString = connString;
            ChoseLast = choseLast;
            SavedCheckBox = saved;
        }
    }
}
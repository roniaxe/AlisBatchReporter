using System;

namespace AlisBatchReporter.Classes
{
    internal class ConnectionString
    {
        public string Env { get; set; }
        public string Database { get; set; }
        public string ConnString { get; set; }

        public ConnectionString(string env, string database, string connectionString)
        {
            ConnString = connectionString;
            Env = env;
            Database = database;
        }
    }
}
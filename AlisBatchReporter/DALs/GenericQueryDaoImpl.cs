using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using AlisBatchReporter.Classes;

namespace AlisBatchReporter.DALs
{
    public abstract class GenericQueryDaoImpl : IGenericQueryDao
    {
        protected string QueryPath;

        protected GenericQueryDaoImpl(string queryPath)
        {
            QueryPath = queryPath;
        }

        public DataTable GetData(string selectCommand)
        {
            DataTable table = null;
            try
            {
                var connectionString = Global.ConnString;
                var dataAdapter = new SqlDataAdapter(selectCommand, connectionString);
                dataAdapter.SelectCommand.CommandTimeout = 120;
                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);
                table = new DataTable {Locale = CultureInfo.InvariantCulture};

                dataAdapter.Fill(table);
            }
            catch (SqlException exception)
            {
                MessageBox.Show(exception.Message);
            }
            return table;
        }

        public DataTable DoQuery()
        {
            var script = File.ReadAllText(QueryPath);
            var convertedScript = EmbedScript(script);
            return GetData(convertedScript);
        }

        public abstract string EmbedScript(string script);
    }
}
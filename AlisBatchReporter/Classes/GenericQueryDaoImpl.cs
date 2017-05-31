using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using AlisBatchReporter.DALs;

namespace AlisBatchReporter.Classes
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
                // Specify a connection string. Replace the given value with 
                // valid connection string for a Northwind SQL Server sample
                // database accessible to your system.
                var connectionString = Global.ChosenConnection;

                // Create a new data adapter based on the specified query.
                var dataAdapter = new SqlDataAdapter(selectCommand, connectionString);

                // Create a command builder to generate SQL update, insert, and
                // delete commands based on selectCommand. These are used to
                // update the database.
                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);

                // Populate a new data table and bind it to the BindingSource.
                table = new DataTable {Locale = CultureInfo.InvariantCulture};
                dataAdapter.Fill(table);
            }
            catch (SqlException)
            {
                MessageBox.Show("To run this example, replace the value of the " +
                                "connectionString variable with a connection string that is " +
                                "valid for your system.");
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
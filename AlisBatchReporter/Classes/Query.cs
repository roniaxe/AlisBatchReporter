using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Windows.Forms;

namespace AlisBatchReporter.Classes
{
    public abstract class Query
    {
        protected string QueryPath;

        protected Query(string queryPath)
        {
            QueryPath = queryPath;
        }

        protected DataTable GetData(string selectCommand)
        {
            DataTable table = null;
            try
            {
                // Specify a connection string. Replace the given value with a
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

        public abstract DataTable DoQuery();
    }
}
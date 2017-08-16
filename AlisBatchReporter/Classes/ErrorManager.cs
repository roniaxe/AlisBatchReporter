using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlisBatchReporter.Classes
{
    public static class QueryManager
    {
        public static async Task<DataTable> Query(string command)
        {
            using (SqlConnection conn = new SqlConnection(Global.ChosenConnection))
            {
                SqlCommand sqlCommand = new SqlCommand(command, conn);
                await conn.OpenAsync();
                sqlCommand.CommandTimeout = 150;
                int result = await sqlCommand.ExecuteNonQueryAsync();
                SqlDataReader reader = await sqlCommand.ExecuteReaderAsync();
                var dataTable = new DataTable();
                dataTable.Load(reader);
                return dataTable;
            }
        }
    }

    public class QueryRepo
    {
        public string QueryName { get; set; }
        public string QueryPath { get; set; }
        public ParamObject Params { get; set; }
        public int ParamCount { get; set; }

        public QueryRepo(string queryName, string queryPath, int paramCount)
        {
            QueryName = queryName;
            QueryPath = queryPath;
            ParamCount = paramCount;
        }

        public static readonly QueryRepo ErrorReport = new QueryRepo(
            "Error Report",
            Path.GetDirectoryName(Application.ExecutablePath)+@"\Resources\SQL\BatchAudit.sql",
            4);

        public static readonly QueryRepo CmaOfPayment = new QueryRepo(
            "Payment CMA",
            Path.GetDirectoryName(Application.ExecutablePath) + @"\Resources\SQL\CmaByBatchRunNo.sql",
            1);

        public static readonly QueryRepo FindObject = new QueryRepo(
            "Find Object",
            Path.GetDirectoryName(Application.ExecutablePath) + @"\Resources\SQL\FindObject.sql",
            3);

        public static readonly QueryRepo EftExport = new QueryRepo(
            "Eft Export",
            Path.GetDirectoryName(Application.ExecutablePath) + @"\Resources\SQL\IEftExport.sql",
            0);

        public static readonly QueryRepo TaskRunningTime = new QueryRepo(
            "Task Running Time",
            Path.GetDirectoryName(Application.ExecutablePath) + @"\Resources\SQL\RunningTime.sql",
            3);

        public static readonly QueryRepo SimpleGba = new QueryRepo(
            "Simble GBA",
            Path.GetDirectoryName(Application.ExecutablePath) + @"\Resources\SQL\SimpleGBA.sql",
            3);

        public static readonly QueryRepo TaskList = new QueryRepo(
            "Task List",
            Path.GetDirectoryName(Application.ExecutablePath) + @"\Resources\SQL\TaskList.sql",
            2);

        public static readonly QueryRepo UnallocatedSuspenseReport = new QueryRepo(
            "Unallocated Suspense",
            Path.GetDirectoryName(Application.ExecutablePath) + @"\Resources\SQL\UnallocatedSuspenseReport.sql",
            2);

        public static readonly QueryRepo ProcessingRateReport = new QueryRepo(
            "Processing Rate",
            Path.GetDirectoryName(Application.ExecutablePath) + @"\Resources\SQL\ProcessingRate.sql",
            2);


        public string QueryDynamication()
        {
            string originalQuery = File.ReadAllText(QueryPath);
            for (int i = 1; i <= ParamCount; i++)
            {
                string paramValue = null;
                switch (i) 
                {
                    case 1:
                        paramValue = Params.Param1;
                        break;
                    case 2:
                        paramValue = Params.Param2;
                        break;
                    case 3:
                        paramValue = Params.Param3;
                        break;
                    case 4:
                        paramValue = Params.Param4;
                        break;
                    case 5:
                        paramValue = Params.Param5;
                        break;
                }
                originalQuery = originalQuery.Replace(@"{Param" + i + "}", paramValue);
            }
            return originalQuery;
        }
    }
}

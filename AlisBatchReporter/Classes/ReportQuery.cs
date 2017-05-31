using System.Data;
using System.IO;

namespace AlisBatchReporter.Classes
{
    internal class ReportQuery : Query
    {
        private readonly string _fromDate;
        private readonly string _toDate;

        public ReportQuery(string queryPath, string fromDate, string toDate) : base(queryPath)
        {           
            _fromDate = fromDate;
            _toDate = toDate;
        }

        public override DataTable DoQuery()
        {
            var script = File.ReadAllText(QueryPath);
            var convertedScript = EmbedScript(script);          
            return GetData(convertedScript);
        }

        

        private string EmbedScript(string script)
        {
            var result = script.Replace("{fromDate}", "'" + _fromDate + "'")
                .Replace("{toDate}", "'" + _toDate + "'");
            return result;
        }
    }
}
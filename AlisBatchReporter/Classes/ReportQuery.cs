using AlisBatchReporter.DALs;

namespace AlisBatchReporter.Classes
{
    internal class ReportQuery : GenericQueryDaoImpl
    {
        private readonly string _fromDate;
        private readonly string _toDate;

        public ReportQuery(string queryPath, string fromDate, string toDate) : base(queryPath)
        {           
            _fromDate = fromDate;
            _toDate = toDate;
        }

        public override string EmbedScript(string script)
        {
            var result = script.Replace("{fromDate}", "'" + _fromDate + "'")
                .Replace("{toDate}", "'" + _toDate + "'");
            return result;
        }
    }
}
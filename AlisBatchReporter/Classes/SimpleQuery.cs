using System.Data;
using System.IO;

namespace AlisBatchReporter.Classes
{
    public class SimpleQuery : Query
    {
        private string _queryPath;
        private readonly string _batchRunNum;

        public SimpleQuery(string queryPath, string batchRunNum) : base(queryPath)
        {
            _queryPath = queryPath;
            _batchRunNum = batchRunNum;
        }
        private string EmbedScript(string script)
        {
            var result = script.Replace("{batchRunNum}", _batchRunNum);
            return result;
        }

        public override DataTable DoQuery()
        {
            var script = File.ReadAllText(QueryPath);
            var convertedScript = EmbedScript(script);
            return GetData(convertedScript);
        }
    }
}
using System.Data;
using System.IO;

namespace AlisBatchReporter.Classes
{
    public class SimpleQuery : GenericQueryDaoImpl
    {
        private readonly string _batchRunNum;

        public SimpleQuery(string queryPath, string batchRunNum) : base(queryPath)
        {
            _batchRunNum = batchRunNum;
        }

        public override string EmbedScript(string script)
        {
            var result = script.Replace("{batchRunNum}", _batchRunNum);
            return result;
        }
    }
}
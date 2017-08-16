using AlisBatchReporter.DALs;

namespace AlisBatchReporter.Classes
{
    public class SimpleQuery : GenericQueryDaoImpl
    {
        private readonly string _batchRunNum;
        private readonly string _taskId;
        private readonly bool _onlyErrors;

        public SimpleQuery(string queryPath, string batchRunNum, string taskId, bool onlyErrors) : base(queryPath)
        {
            _batchRunNum = batchRunNum;
            _taskId = taskId;
            _onlyErrors = onlyErrors;
        }

        public override string EmbedScript(string script)
        {
            var result = script.Replace("{batchRunNum}", _batchRunNum)
                .Replace("{taskId}", _taskId);
            if (!_onlyErrors)
            {
                result = result.Replace("AND GBA.ENTRY_TYPE IN (5,6)", "");
            }
            return result;
        }
    }
}
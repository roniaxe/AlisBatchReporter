namespace AlisBatchReporter.Classes
{
    public class SimpleQuery : GenericQueryDaoImpl
    {
        private readonly string _batchRunNum;
        private readonly string _taskId;

        public SimpleQuery(string queryPath, string batchRunNum, string taskId) : base(queryPath)
        {
            _batchRunNum = batchRunNum;
            _taskId = taskId;
        }

        public override string EmbedScript(string script)
        {
            var result = script.Replace("{batchRunNum}", _batchRunNum)
                .Replace("{taskId}", _taskId);
            return result;
        }
    }
}
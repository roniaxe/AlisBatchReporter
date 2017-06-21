using AlisBatchReporter.DALs;

namespace AlisBatchReporter.Classes
{
    class SimpleExportQuery : GenericQueryDaoImpl
    {
        private readonly string _key;

        public SimpleExportQuery(string queryPath, string key) : base(queryPath)
        {
            _key = key;
        }

        public override string EmbedScript(string script)
        {
            var result = script.Replace("{key}", _key);
            return result;
        }
    }
}

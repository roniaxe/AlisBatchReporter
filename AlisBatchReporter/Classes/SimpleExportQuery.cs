using AlisBatchReporter.DALs;

namespace AlisBatchReporter.Classes
{
    class SimpleExportQuery : GenericQueryDaoImpl
    {
        private readonly string _key;
        private readonly string _keyType;

        public SimpleExportQuery(string queryPath, string key, string keyType) : base(queryPath)
        {
            _key = key;
            _keyType = keyType;
        }

        public override string EmbedScript(string script)
        {
            if (_keyType.Equals("Internal"))
            {
                script = script.Replace(@"AND IUSR.EXTERNAL_DESTINATION_ID LIKE '{key}'", "");
            }
            else
            {
               script = script.Replace(@"AND IUSR.DESTINATION_ID = {key}", "");
            }
            var result = script.Replace("{key}", _key);
            return result;
        }
    }
}

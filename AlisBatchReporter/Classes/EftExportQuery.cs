using AlisBatchReporter.DALs;

namespace AlisBatchReporter.Classes
{
    class EftExportQuery : GenericQueryDaoImpl
    {
        public EftExportQuery(string queryPath) : base(queryPath)
        {
        }

        public override string EmbedScript(string script)
        {
            return script;
        }
    }
}

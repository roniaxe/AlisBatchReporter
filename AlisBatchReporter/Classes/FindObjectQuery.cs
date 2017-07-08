using System.Windows.Forms;
using AlisBatchReporter.DALs;

namespace AlisBatchReporter.Classes
{
    class FindObjectQuery : GenericQueryDaoImpl
    {
        private readonly string _entityTextBox1;
        private readonly string _entityTextBox2;
        private readonly string _entityTextBox3;
        private readonly string _fromDate;
        private readonly string _toDate;

        public FindObjectQuery(string queryPath) : base(queryPath)
        {
        }

        public FindObjectQuery(string queryPath, string fromDate, string toDate, string entityTextBox1, string entityTextBox2, string entityTextBox3) : this(queryPath)
        {
            this._fromDate = fromDate;
            this._toDate = toDate;
            this._entityTextBox1 = entityTextBox1;
            this._entityTextBox2 = entityTextBox2;
            this._entityTextBox3 = entityTextBox3;
        }

        public override string EmbedScript(string script)
        {
            var result = script.Replace("{fromDate}", "'" + _fromDate + "'")
                .Replace("{toDate}", "'" + _toDate + "'");
            var entities = "(GBA.primary_key";
            if (!string.IsNullOrEmpty(_entityTextBox1))
            {
                entities += $@" LIKE '{_entityTextBox1}'";
            }
            if (!string.IsNullOrEmpty(_entityTextBox2))
            {
                entities += $@" OR GBA.primary_key LIKE '{_entityTextBox2}'";
            }
            if (!string.IsNullOrEmpty(_entityTextBox3))
            {
                entities += $@" OR GBA.primary_key LIKE '{_entityTextBox3}'";
            }
            result = result.Replace("{entities}", entities+")");
            return result;
        }
    }
}

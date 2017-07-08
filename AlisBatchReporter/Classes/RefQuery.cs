using System.Windows.Forms;
using AlisBatchReporter.DALs;

namespace AlisBatchReporter.Classes
{
    class RefQuery : GenericQueryDaoImpl
    {
        private readonly string _value;


        public RefQuery(string queryPath) : base(queryPath)
        {
        }

        public RefQuery(string queryPath, string value) : this(queryPath)
        {
            this._value = value;
        }

        public override string EmbedScript(string script)
        {
            script = script.Replace("{value}", _value);
            return script;
        }
    }
}

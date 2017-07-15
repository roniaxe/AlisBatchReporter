using AlisBatchReporter.DALs;

namespace AlisBatchReporter.Classes
{
    internal class ReportQuery : GenericQueryDaoImpl
    {
        private readonly string _fromDate;
        private readonly string _toDate;
        private readonly string _polFilter;
        private readonly bool _typeRadioButton;

        public ReportQuery(string queryPath, string fromDate, string toDate, string polFilter, bool typeRadioButton) : base(queryPath)
        {           
            _fromDate = fromDate;
            _toDate = toDate;
            _polFilter = polFilter;
            _typeRadioButton = typeRadioButton;
        }

        public override string EmbedScript(string script)
        {
            var result = script.Replace("{fromDate}", "'" + _fromDate + "'")
                .Replace("{toDate}", "'" + _toDate + "'");
            if (!string.IsNullOrEmpty(_polFilter))
            {
                result = result.Replace("{polFilter}", _polFilter);
                if (!_typeRadioButton)
                {
                    result = result.Replace(@"AND gba.entry_type IN 
   (
      5,
      6
   )", "");
                }
            }
            else
            {
                result = result.Replace("AND gba.primary_key = {polFilter}", "").
                    Replace("AND gba.primary_key LIKE '{polFilter}'","");
            }
            return result;
        }
    }
}
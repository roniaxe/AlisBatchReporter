using System.Collections.Generic;

namespace AlisBatchReporter.Classes
{
    public class ArcvalValuesType7 : Values
    {
        #region Constructor
        private ArcvalValuesType7(int idxValue, string name, bool toIgnore = false, bool toRound = false) : base(idxValue, name, toIgnore, toRound)
        {
        }
        #endregion

        #region StaticProperties
        public static readonly Values RecInfo = new ArcvalValuesType7(11, "REC-INFO");
        public static readonly Values NumSelected = new ArcvalValuesType7(12, "NUM-SELECT");
        public static readonly Values NumFields = new ArcvalValuesType7(13, "NUM-FIELDS");
        public static readonly Values Extra = new ArcvalValuesType7(14, "EXTRA");

        public static Values[] ValusArr { get; } =
        {
            CompanyCode,
            PlanCode,
            InsuredGender,
            RiskClass,
            SubStdIndex,
            Filler,
            WaiverPremInd,
            IssueDate,
            InsuredIssueAge,
            ContractNum,
            RecordType,
            RecInfo,
            NumSelected,
            NumFields,
            Extra
        };      
        #endregion
    }
}
using System.Collections.Generic;

namespace AlisBatchReporter.Classes
{
    class ArcvalValuesStatusRpu : ArcvalValuesType1
    {
        #region Constructor
        private ArcvalValuesStatusRpu(int idxValue, string name, bool ignore = false, bool toRound = false) : base(idxValue, name, ignore, toRound)
        {
        }
        #endregion

        #region StaticProperties
        public static readonly Values PremiumMode = new ArcvalValuesType1(13, "PREM-MODE");
        public static readonly Values Filler2 = new ArcvalValuesStatusRpu(14, "FILLER2", true); //9
        public static readonly Values NumOfUnits = new ArcvalValuesStatusRpu(15, "UNITS"); //8
        public static readonly Values CashValPdUp = new ArcvalValuesStatusRpu(16, "CASH-VALUE-PDUP"); //9 
        public static readonly Values Filler3 = new ArcvalValuesStatusRpu(17, "FILLER3", true); //2 
        public static readonly Values TermDate = new ArcvalValuesStatusRpu(18, "TERM-DATE"); //8 

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
            StatusCode,
            State,
            PremiumMode,
            Filler2,
            NumOfUnits,
            CashValPdUp,
            Filler3,
            TermDate
        }; 
        #endregion
    }
}

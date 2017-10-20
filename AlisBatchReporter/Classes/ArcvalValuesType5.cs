using System.Collections.Generic;

namespace AlisBatchReporter.Classes
{
    class ArcvalValuesType5 : Values
    {
        #region Constructor

        private ArcvalValuesType5(int idxValue, string name, bool toIgnore = false, bool toRound = false) : base(idxValue, name, toIgnore, toRound)
        {
        }

        #endregion

        #region Properties
        public static readonly Values CovPlancode = new ArcvalValuesType5(11, "COV-PLANCODE", true);
        public static readonly Values RdrWp = new ArcvalValuesType5(12, "RDR-WP");
        public static readonly Values RdrGender = new ArcvalValuesType5(13, "RDR-GENDER");
        public static readonly Values RdrRisk = new ArcvalValuesType5(14, "RDR-RISK");
        public static readonly Values RdrAge = new ArcvalValuesType5(15, "RDR-AGE");
        public static readonly Values RdrSub = new ArcvalValuesType5(16, "RDR-SUB");
        public static readonly Values RdrEff = new ArcvalValuesType5(17, "RDR-EFF");
        public static readonly Values RdrExp = new ArcvalValuesType5(18, "RDR-EXP");
        public static readonly Values RdrFace = new ArcvalValuesType5(19, "RDR-FACE", false, true);
        public static readonly Values RdrStatRes = new ArcvalValuesType5(20, "RDR-STAT-RES");
        public static readonly Values RdrTaxRes = new ArcvalValuesType5(21, "RDR-TAX-RES");
        public static readonly Values RdrGrossPrem = new ArcvalValuesType5(22, "RDR-GROSS-PREM", false, true);
        public static readonly Values RdrModePrem = new ArcvalValuesType5(23, "RDR-MODE-PREMIUM", false, true);
        public static readonly Values RdrCashValue = new ArcvalValuesType5(24, "RDR-CASH-VALUE", false, true);
        public static readonly Values Extra = new ArcvalValuesType5(25, "EXTRA");
        #endregion

        #region StaticProperties
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
            CovPlancode,
            RdrWp,
            RdrGender,
            RdrRisk,
            RdrAge,
            RdrSub,
            RdrEff,
            RdrExp,
            RdrFace,
            RdrStatRes,
            RdrTaxRes,
            RdrGrossPrem,
            RdrModePrem,
            RdrCashValue,
            Extra
        };        
        #endregion
    }
}

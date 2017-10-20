using System.Collections.Generic;

namespace AlisBatchReporter.Classes
{
    class ArcvalValuesType6A : Values
    {
        #region Constructor
        private ArcvalValuesType6A(int idxValue, string name, bool toIgnore = false, bool toRound = false) : base(idxValue, name, toIgnore, toRound)
        {
        }
        #endregion

        #region StaticProperties
        public static readonly Values RecInfo = new ArcvalValuesType6A(11, "REC-INFO");
        public static readonly Values RdrPlancode = new ArcvalValuesType6A(12, "RDR-PLANCODE", true);
        public static readonly Values RdrWp = new ArcvalValuesType6A(13, "RDR-WP");
        public static readonly Values CovPlancode = new ArcvalValuesType6A(14, "COV-PLANCODE", true);
        public static readonly Values CovId = new ArcvalValuesType6A(15, "COV-ID", true);
        public static readonly Values RdrGender = new ArcvalValuesType6A(16, "RDR-GENDER");
        public static readonly Values RdrRisk = new ArcvalValuesType6A(17, "RDR-RISK");
        public static readonly Values RdrAge = new ArcvalValuesType6A(18, "RDR-AGE");
        public static readonly Values RdrSub = new ArcvalValuesType6A(19, "RDR-SUB");
        public static readonly Values RdrEff = new ArcvalValuesType6A(20, "RDR-EFF");
        public static readonly Values RdrExp = new ArcvalValuesType6A(21, "RDR-EXP");
        public static readonly Values RdrFace = new ArcvalValuesType6A(22, "RDR-FACE");
        public static readonly Values RdrGrossPrem = new ArcvalValuesType6A(23, "RDR-GROSS-PREM", false, true);
        public static readonly Values RdrModePrem = new ArcvalValuesType6A(24, "RDR-MODE-PREMIUM", false, true);
        public static readonly Values Extra = new ArcvalValuesType6A(25, "EXTRA");

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
            RdrPlancode,
            RdrWp,
            CovPlancode,
            CovId,
            RdrGender,
            RdrRisk,
            RdrAge,
            RdrSub,
            RdrEff,
            RdrExp,
            RdrFace,
            RdrGrossPrem,
            RdrModePrem,
            Extra
        };      
        #endregion
    }
}

using System.Collections.Generic;

namespace AlisBatchReporter.Classes
{
    class ArcvalValuesType5 : Values
    {
        public static readonly Values CoCode = new ArcvalValuesType5(0, "CO-CODE");
        public static readonly Values ConPlanCode = new ArcvalValuesType5(1, "CON-PLANCODE", true);
        public static readonly Values Gender = new ArcvalValuesType5(2, "GENDER");
        public static readonly Values RiskClass = new ArcvalValuesType5(3, "RISK-CLASS");
        public static readonly Values SsTable = new ArcvalValuesType5(4, "SS-TABLE");
        public static readonly Values Filler = new ArcvalValuesType5(5, "FILLER");
        public static readonly Values WaiverSw = new ArcvalValuesType5(6, "WAIVER-SW");
        public static readonly Values IssDate = new ArcvalValuesType5(7, "ISS-DATE");
        public static readonly Values IssAge = new ArcvalValuesType5(8, "ISS-AGE");
        public static readonly Values ConNum = new ArcvalValuesType5(9, "CON-NUM");
        public static readonly Values RecType = new ArcvalValuesType5(10, "REC-TYPE");
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


        private ArcvalValuesType5(int idxValue, string name, bool toIgnore = false, bool toRound = false) : base(idxValue, name, toIgnore, toRound)
        {
        }
        public ArcvalValuesType5() { }
        public override IEnumerable<Values> GetValues
        {
            get
            {
                yield return CoCode;
                yield return ConPlanCode;
                yield return Gender;
                yield return RiskClass;
                yield return SsTable;
                yield return Filler;
                yield return WaiverSw;
                yield return IssDate;
                yield return IssAge;
                yield return ConNum;
                yield return RecType;
                yield return CovPlancode;
                yield return RdrWp;
                yield return RdrGender;
                yield return RdrRisk;
                yield return RdrAge;
                yield return RdrSub;
                yield return RdrEff;
                yield return RdrExp;
                yield return RdrFace;
                yield return RdrStatRes;
                yield return RdrTaxRes;
                yield return RdrGrossPrem;
                yield return RdrModePrem;
                yield return RdrCashValue;
                yield return Extra;
            }
        }
    }
}

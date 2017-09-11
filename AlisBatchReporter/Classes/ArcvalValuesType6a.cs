﻿using System.Collections.Generic;

namespace AlisBatchReporter.Classes
{
    class ArcvalValuesType6A : Values
    {
        public static readonly Values CoCode = new ArcvalValuesType6A(0, "CO-CODE");
        public static readonly Values ConPlanCode = new ArcvalValuesType6A(1, "CON-PLANCODE", true);
        public static readonly Values Gender = new ArcvalValuesType6A(2, "GENDER");
        public static readonly Values RiskClass = new ArcvalValuesType6A(3, "RISK-CLASS");
        public static readonly Values SsTable = new ArcvalValuesType6A(4, "SS-TABLE");
        public static readonly Values Filler = new ArcvalValuesType6A(5, "FILLER");
        public static readonly Values WaiverSw = new ArcvalValuesType6A(6, "WAIVER-SW");
        public static readonly Values IssDate = new ArcvalValuesType6A(7, "ISS-DATE");
        public static readonly Values IssAge = new ArcvalValuesType6A(8, "ISS-AGE");
        public static readonly Values ConNum = new ArcvalValuesType6A(9, "CON-NUM");
        public static readonly Values RecType = new ArcvalValuesType6A(10, "REC-TYPE");
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


        private ArcvalValuesType6A(int idxValue, string name, bool toIgnore = false, bool toRound = false) : base(idxValue, name, toIgnore, toRound)
        {
        }
        public ArcvalValuesType6A() { }
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
                yield return RecInfo;
                yield return RdrPlancode;
                yield return RdrWp;
                yield return CovPlancode;
                yield return CovId;
                yield return RdrGender;
                yield return RdrRisk;
                yield return RdrAge;
                yield return RdrSub;
                yield return RdrEff;
                yield return RdrExp;
                yield return RdrFace;
                yield return RdrGrossPrem;
                yield return RdrModePrem;
                yield return Extra;
            }
        }
    }
}

using System.Collections.Generic;

namespace AlisBatchReporter.Classes
{
    public class ArcvalValuesType7 : Values
    {
        public static readonly Values CoCode = new ArcvalValuesType7(0, "CO-CODE");
        public static readonly Values ConPlanCode = new ArcvalValuesType7(1, "CON-PLANCODE", true);
        public static readonly Values Gender = new ArcvalValuesType7(2, "GENDER");
        public static readonly Values RiskClass = new ArcvalValuesType7(3, "RISK-CLASS");
        public static readonly Values SsTable = new ArcvalValuesType7(4, "SS-TABLE");
        public static readonly Values Filler = new ArcvalValuesType7(5, "FILLER");
        public static readonly Values WaiverSw = new ArcvalValuesType7(6, "WAIVER-SW");
        public static readonly Values IssDate = new ArcvalValuesType7(7, "ISS-DATE");
        public static readonly Values IssAge = new ArcvalValuesType7(8, "ISS-AGE");
        public static readonly Values ConNum = new ArcvalValuesType7(9, "CON-NUM");
        public static readonly Values RecType = new ArcvalValuesType7(10, "REC-TYPE");
        public static readonly Values RecInfo = new ArcvalValuesType7(11, "REC-INFO");
        public static readonly Values NumSelected = new ArcvalValuesType7(12, "NUM-SELECT");
        public static readonly Values NumFields = new ArcvalValuesType7(13, "NUM-FIELDS");
        public static readonly Values Extra = new ArcvalValuesType7(14, "EXTRA");

        public static Values[] ValusArr { get; } =
        {
            CoCode,
            ConPlanCode,
            Gender,
            RiskClass,
            SsTable,
            Filler,
            WaiverSw,
            IssDate,
            IssAge,
            ConNum,
            RecType,
            RecInfo,
            NumSelected,
            NumFields,
            Extra
        };

        private ArcvalValuesType7(int idxValue, string name, bool toIgnore = false, bool toRound=false) : base(idxValue, name, toIgnore, toRound)
        {
        }
        public ArcvalValuesType7() { }
        public override IEnumerable<Values> GetValues {
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
                yield return NumSelected;
                yield return NumFields;
                yield return Extra;
            }
        }
    }
}
using System.Collections.Generic;

namespace AlisBatchReporter.Classes
{
    class ArcvalValuesType4 : Values
    {
        private ArcvalValuesType4(int idxValue, string name, bool ignore = false, bool toRound = false) : base(idxValue, name, ignore, toRound)
        {
        }

        public static readonly Values CompanyCode = new ArcvalValuesType4(0, "CO-CODE");
        public static readonly Values PlanCode = new ArcvalValuesType4(1, "CON-PLANCODE", true);
        public static readonly Values InsuredGender = new ArcvalValuesType4(2, "GENDER");
        public static readonly Values RiskClass = new ArcvalValuesType4(3, "RISK-CLASS");
        public static readonly Values SubStdIndex = new ArcvalValuesType4(4, "SS-TABLE");
        public static readonly Values Filler = new ArcvalValuesType4(5, "FILLER1", true);
        public static readonly Values WaiverPremInd = new ArcvalValuesType4(6, "WAIVER-SW");
        public static readonly Values IssueDate = new ArcvalValuesType4(7, "ISS-DATE");
        public static readonly Values InsuredIssueAge = new ArcvalValuesType4(8, "ISS-AGE");
        public static readonly Values ContractNum = new ArcvalValuesType4(9, "CON-NUM");
        public static readonly Values RecordType = new ArcvalValuesType4(10, "REC-TYPE");
        public static readonly Values StatusCode = new ArcvalValuesType4(11, "CON-STATUS");
        public static readonly Values State = new ArcvalValuesType4(12, "STATE-CODE");
        public static readonly Values PremiumMode = new ArcvalValuesType4(13, "PREM-MODE");
        public static readonly Values Filler2 = new ArcvalValuesType4(14, "FILLER2", true); //9
        public static readonly Values NumOfUnits = new ArcvalValuesType4(15, "UNITS"); //8
        public static readonly Values CashValPdUp = new ArcvalValuesType4(16, "CASH-VALUE-PDUP"); //9 
        public static readonly Values Filler3 = new ArcvalValuesType4(17, "FILLER3", true); //2 
        public static readonly Values TermDate = new ArcvalValuesType4(18, "TERM-DATE"); //8 

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

        public ArcvalValuesType4() { }

        public override IEnumerable<Values> GetValues
        {
            get
            {
                yield return CompanyCode;
                yield return PlanCode;
                yield return InsuredGender;
                yield return RiskClass;
                yield return SubStdIndex;
                yield return WaiverPremInd;
                yield return IssueDate;
                yield return InsuredIssueAge;
                yield return ContractNum;
                yield return RecordType;
                yield return StatusCode;
                yield return State;
                yield return PremiumMode;
                yield return Filler2;
                yield return NumOfUnits;
                yield return CashValPdUp;
                yield return Filler3;
                yield return TermDate;
            }
        }
    }
}

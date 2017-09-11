using System.Collections.Generic;

namespace AlisBatchReporter.Classes
{
    public class ArcvalValues : Values
    {
        public static readonly Values CompanyCode = new ArcvalValues(0, "CO-CODE");
        public static readonly Values PlanCode = new ArcvalValues(1, "CON-PLANCODE", true);
        public static readonly Values InsuredGender = new ArcvalValues(2, "GENDER");
        public static readonly Values RiskClass = new ArcvalValues(3, "RISK-CLASS");
        public static readonly Values SubStdIndex = new ArcvalValues(4, "SS-TABLE");
        public static readonly Values WaiverPremInd = new ArcvalValues(5, "FILLER");
        public static readonly Values IssueDate = new ArcvalValues(6, "WAIVER-SW");
        public static readonly Values InsuredIssueAge = new ArcvalValues(7, "ISS-DATE");
        public static readonly Values ContractNum = new ArcvalValues(8, "ISS-AGE");
        public static readonly Values RecordType = new ArcvalValues(9, "CON-NUM");
        public static readonly Values StatusCode = new ArcvalValues(10, "REC-TYPE");
        public static readonly Values State = new ArcvalValues(11, "CON-STATUS");
        public static readonly Values PremiumCode = new ArcvalValues(12, "STA I E-CODE");
        public static readonly Values GrossPrem = new ArcvalValues(13, "PREM-MODE");
        public static readonly Values NumOfUnits = new ArcvalValues(14, "GROSS-PREM",false,true);
        public static readonly Values CashValue = new ArcvalValues(15, "UNITS");
        public static readonly Values ModalGrossPremium = new ArcvalValues(16, "CASH-VALUE", false, true);
        public static readonly Values PaidToDate = new ArcvalValues(17, "MODE-PREMIUM", false, true);
        public static readonly Values SubStdPremType = new ArcvalValues(18, "PAID-TO-DATE");
        public static readonly Values SubStdExtra = new ArcvalValues(19, "SS-TYPE");
        public static readonly Values SubStdExtraAnnPrem = new ArcvalValues(20, "EXTRA");

        private ArcvalValues(int idxValue, string name, bool ignore = false, bool toRound = false) : base(idxValue, name, ignore, toRound)
        {
        }

        public ArcvalValues() { }

        public override IEnumerable<Values> GetValues {
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
                yield return PremiumCode;
                yield return GrossPrem;
                yield return NumOfUnits;
                yield return CashValue;
                yield return ModalGrossPremium;
                yield return PaidToDate;
                yield return SubStdPremType;
                yield return SubStdExtra;
                yield return SubStdExtraAnnPrem;
            }
        }
    }
}

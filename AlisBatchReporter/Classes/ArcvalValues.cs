namespace AlisBatchReporter.Classes
{
    class ArcvalValues : ArcvalValuesType1
    {
        public static readonly Values PremiumMode = new ArcvalValues(13, "PREM-MODE");
        public static readonly Values GrossPrem = new ArcvalValues(14, "GROSS-PREM",false,true);
        public static readonly Values NumOfUnits = new ArcvalValues(15, "UNITS");
        public static readonly Values CashValue = new ArcvalValues(16, "CASH-VALUE", false, true);
        public static readonly Values ModalGrossPremium = new ArcvalValues(17, "MODE-PREMIUM", false, true);
        public static readonly Values PaidToDate = new ArcvalValues(18, "PAID-TO-DATE");
        public static readonly Values SubStdPremType = new ArcvalValues(19, "SS-TYPE");
        public static readonly Values SubStdExtra = new ArcvalValues(20, "EXTRA");

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
            GrossPrem,
            NumOfUnits,
            CashValue,
            ModalGrossPremium,
            PaidToDate,
            SubStdPremType,
            SubStdExtra
        };

        private ArcvalValues(int idxValue, string name, bool ignore = false, bool toRound = false) : base(idxValue, name, ignore, toRound)
        {
        }
    }
}

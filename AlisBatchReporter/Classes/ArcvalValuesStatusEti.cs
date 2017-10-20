namespace AlisBatchReporter.Classes
{
    class ArcvalValuesStatusEti : ArcvalValuesType1
    {
        #region Constructor
        public ArcvalValuesStatusEti(int idxValue, string name, bool ignore = false, bool toRound = false) : base(idxValue, name, ignore, toRound)
        {
        }
        #endregion

        #region StaticProperties
        public static readonly Values PureEndowmentAmt = new ArcvalValuesStatusEti(14, "PURE-ENDOWMENT-AMT", true); //7
        public static readonly Values Filler2 = new ArcvalValuesStatusEti(15, "FILLER", true); //3
        public static readonly Values NumOfUnits = new ArcvalValuesStatusEti(16, "UNITS"); //8
        public static readonly Values CashValExtended = new ArcvalValuesStatusEti(17, "CASH-VALUE-EXTENDED"); //9 
        public static readonly Values ExpireDateEti = new ArcvalValuesStatusEti(18, "EXPIRE-DATE-ETI", true); //8

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
            PureEndowmentAmt,
            Filler2,
            NumOfUnits,
            CashValExtended,
            ExpireDateEti
        }; 
        #endregion
    }
}

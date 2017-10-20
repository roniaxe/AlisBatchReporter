using System;
using System.Collections.Generic;
using System.Linq;

namespace AlisBatchReporter.Classes
{
    public  class Values
    {
        #region Constructor
        protected Values(int idxValue, string name, bool toIgnore = false, bool toRound = false)
        {
            IdxValue = idxValue;
            Name = name;
            ToIgnore = toIgnore;
            ToRound = toRound;
        }
        #endregion

        #region StaticProperties
        public static readonly Values CompanyCode = new Values(0, "CO-CODE");
        public static readonly Values PlanCode = new Values(1, "CON-PLANCODE", true);
        public static readonly Values InsuredGender = new Values(2, "GENDER");
        public static readonly Values RiskClass = new Values(3, "RISK-CLASS");
        public static readonly Values SubStdIndex = new Values(4, "SS-TABLE");
        public static readonly Values Filler = new Values(5, "FILLER1", true);
        public static readonly Values WaiverPremInd = new Values(6, "WAIVER-SW");
        public static readonly Values IssueDate = new Values(7, "ISS-DATE");
        public static readonly Values InsuredIssueAge = new Values(8, "ISS-AGE");
        public static readonly Values ContractNum = new Values(9, "CON-NUM");
        public static readonly Values RecordType = new Values(10, "REC-TYPE");
        #endregion

        #region Properties
        public int IdxValue { get; }
        public string Name { get; }
        public bool ToIgnore { get; }
        public bool ToRound { get; } 
        #endregion
    }
}

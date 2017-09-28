using System.Collections.Generic;
using System.Linq;

namespace AlisBatchReporter.Classes
{
    public class IrfRecord
    {
        #region Properties

        public string Key { get; set; }
        public List<IrfRecordProperty> Body { get; set; }

        #endregion

    }

    public class IrfRecordProperty
    {
        public class PropertySettings
        {
            public int Index { get; set; }
            public string Name { get; set; }
            public bool Intable { get; set; }
            public bool ToIgnore { get; set; }

            public PropertySettings(int index, string name, bool intable = false, bool toIgnore = false)
            {
                Index = index;
                Name = name;
                Intable = intable;
                ToIgnore = toIgnore;
            }
        }

        #region Properties

        public static readonly PropertySettings CompanyCd = new PropertySettings(0, "Company Cd");
        public static readonly PropertySettings PolNo = new PropertySettings(1, "Policy No");
        public static readonly PropertySettings MembershipNo = new PropertySettings(2, "Membership No");
        public static readonly PropertySettings Lob = new PropertySettings(3, "LOB");
        public static readonly PropertySettings Region = new PropertySettings(4, "Region");
        public static readonly PropertySettings WritingAgency = new PropertySettings(5, "Writing Agency");
        public static readonly PropertySettings WritingAgent = new PropertySettings(6, "Writing Agent");
        public static readonly PropertySettings PrimaryAgentFlag = new PropertySettings(7, "Primary Agent Flag");
        public static readonly PropertySettings Commission = new PropertySettings(8, "Commission", true);
        public static readonly PropertySettings Servicing = new PropertySettings(9, "Servicing");
        public static readonly PropertySettings PlanCode = new PropertySettings(10, "Plan Code", false, true);
        public static readonly PropertySettings PlanName = new PropertySettings(11, "Plan Name", false, true);
        public static readonly PropertySettings IssuedAge = new PropertySettings(12, "Issued Age", true);
        public static readonly PropertySettings IssuedGender = new PropertySettings(13, "Issued Gender", false, true);
        public static readonly PropertySettings LastName = new PropertySettings(14, "Last Name", false, true);
        public static readonly PropertySettings FirstName = new PropertySettings(15, "First Name", false, true);
        public static readonly PropertySettings MiddleName = new PropertySettings(16, "Middle Name", false, true);
        public static readonly PropertySettings PolicyIssueDate = new PropertySettings(17, "Policy Issue Date");
        public static readonly PropertySettings PaidToPaid = new PropertySettings(18, "Paid To Paid");
        public static readonly PropertySettings CurrentCashValue = new PropertySettings(19, "Current Cash Value" , true);
        public static readonly PropertySettings ModalPremium = new PropertySettings(20, "Modal Premium", true);
        public static readonly PropertySettings FaceValue = new PropertySettings(21, "Face Value", true);
        public static readonly PropertySettings SpouseFaceValue = new PropertySettings(22, "Spouse Face Value", true);
        public static readonly PropertySettings ActiveFlag = new PropertySettings(23, "Active Flag");
        public static readonly PropertySettings PolicyStatus = new PropertySettings(24, "Policy Status");
        public static readonly PropertySettings PaymentMode = new PropertySettings(25, "Payment Mode");
        public static readonly PropertySettings PaymentForm = new PropertySettings(26, "Payment Form");
        public PropertySettings Settings { get; set; }
        public string Value { get; set; }

        #endregion

        #region Methods

        public PropertySettings GetValue(int key) => Values().First(v => v.Index == key);

        public IEnumerable<PropertySettings> Values()
        {
            yield return CompanyCd;
            yield return PolNo;
            yield return MembershipNo;
            yield return Lob;
            yield return Region;
            yield return WritingAgency;
            yield return WritingAgent;
            yield return PrimaryAgentFlag;
            yield return Commission;
            yield return Servicing;
            yield return PlanCode;
            yield return PlanName;
            yield return IssuedAge;
            yield return IssuedGender;
            yield return LastName;
            yield return FirstName;
            yield return MiddleName;
            yield return PolicyIssueDate;
            yield return PaidToPaid;
            yield return CurrentCashValue;
            yield return ModalPremium;
            yield return FaceValue;
            yield return SpouseFaceValue;
            yield return ActiveFlag;
            yield return PolicyStatus;
            yield return PaymentMode;
            yield return PaymentForm;
        }

        #endregion
    }
}

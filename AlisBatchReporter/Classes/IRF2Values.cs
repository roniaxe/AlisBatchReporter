using System.Collections.Generic;
using System.Linq;

namespace AlisBatchReporter.Classes
{
    abstract class IrfValues
    {
        public int IdxValue { get; }
        public bool Intable { get; }
        public string Name { get; }
        public bool Ignore { get; }

        protected IrfValues(int idxValue, bool intable, string name, bool ignore = false)
        {
            IdxValue = idxValue;
            Intable = intable;
            Name = name;
            Ignore = ignore;
        }
        protected IrfValues()
        {
        }

        public abstract IEnumerable<IrfValues> Values();

        public IrfValues GetValue(int key) => Values().First(v => v.IdxValue == key);
    }
    class Irf2Values : IrfValues
    {
        public static readonly IrfValues CompanyCd = new Irf2Values(0, false, "Company Cd");
        public static readonly IrfValues PolNo = new Irf2Values(1, false, "Policy No");
        public static readonly IrfValues MembershipNo = new Irf2Values(2, false, "Membership No");
        public static readonly IrfValues LOB = new Irf2Values(3, false, "LOB");
        public static readonly IrfValues Region = new Irf2Values(4, false,"Region");
        public static readonly IrfValues WritingAgency = new Irf2Values(5, false, "Writing Agency");
        public static readonly IrfValues WritingAgent = new Irf2Values(6, false, "Writing Agent");
        public static readonly IrfValues PrimaryAgentFlag = new Irf2Values(7, false, "Primary Agent Flag");
        public static readonly IrfValues Commission = new Irf2Values(8, true, "Commission");
        public static readonly IrfValues Servicing = new Irf2Values(9, false, "Servicing");
        public static readonly IrfValues PlanCode = new Irf2Values(10, false, "Plan Code", true);
        public static readonly IrfValues PlanName = new Irf2Values(11, false, "Plan Name", true);
        public static readonly IrfValues IssuedAge = new Irf2Values(12, true, "Issued Age");
        public static readonly IrfValues IssuedGender = new Irf2Values(13, false, "Issued Gender");
        public static readonly IrfValues LastName = new Irf2Values(14, false, "Last Name", true);
        public static readonly IrfValues FirstName = new Irf2Values(15, false, "First Name", true);
        public static readonly IrfValues MiddleName = new Irf2Values(16, false, "Middle Name", true);
        public static readonly IrfValues PolicyIssueDate = new Irf2Values(17, false, "Policy Issue Date");
        public static readonly IrfValues PaidToPaid = new Irf2Values(18, false, "Paid To Paid");
        public static readonly IrfValues CurrentCashValue = new Irf2Values(19, true, "Current Cash Value");
        public static readonly IrfValues ModalPremium = new Irf2Values(20, true, "Modal Premium");
        public static readonly IrfValues FaceValue = new Irf2Values(21, true, "Face Value");
        public static readonly IrfValues SpouseFaceValue = new Irf2Values(22, true, "Spouse Face Value");
        public static readonly IrfValues ActiveFlag = new Irf2Values(23, false, "Active Flag");
        public static readonly IrfValues PolicyStatus = new Irf2Values(24, false, "Policy Status");
        public static readonly IrfValues PaymentMode = new Irf2Values(25, false, "Payment Mode");
        public static readonly IrfValues PaymentForm = new Irf2Values(26, false, "Payment Form");

        public Irf2Values(int idxValue, bool intable, string name, bool ignore = false) : base(idxValue, intable, name, ignore)
        {

        }

        public Irf2Values()
        {
            
        }

        public override IEnumerable<IrfValues> Values()
        {

                yield return CompanyCd;
                yield return PolNo;
                yield return MembershipNo;
                yield return LOB;
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
    }

    class Irf1Values : IrfValues
    {
        public static readonly IrfValues CompanyCd = new Irf1Values(0, false, "Company Cd");
        public static readonly IrfValues PolNo = new Irf1Values(1, false, "Policy No");
        public static readonly IrfValues WritingAgency = new Irf1Values(2, false, "Writing Agency");
        public static readonly IrfValues WritingAgent = new Irf1Values(3, false, "Writing Agent");
        public static readonly IrfValues LOB = new Irf1Values(4, false, "LOB");
        public static readonly IrfValues ActiveFlag = new Irf1Values(5, false, "Active Flag");
        public static readonly IrfValues PolicyStatus = new Irf1Values(6, false, "Policy Status");
        public static readonly IrfValues MembershipNo = new Irf1Values(7, false, "Membership No");       
        public static readonly IrfValues PolicyIssueDate = new Irf1Values(8, false, "Policy Issue Date");
        


        public Irf1Values(int idxValue, bool intable, string name, bool ignore = false) : base(idxValue, intable, name, ignore)
        {

        }

        public Irf1Values()
        {

        }

        public override IEnumerable<IrfValues> Values()
        {
            yield return CompanyCd;
            yield return PolNo;
            yield return WritingAgency;
            yield return WritingAgent;
            yield return LOB;
            yield return ActiveFlag;
            yield return PolicyStatus;
            yield return MembershipNo;
            yield return PolicyIssueDate;
        }
    }
}

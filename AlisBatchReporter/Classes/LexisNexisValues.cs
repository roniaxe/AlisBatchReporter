using System.Collections.Generic;
using System.Linq;

namespace AlisBatchReporter.Classes
{
    class LexisNexisValues
    {
        public static readonly LexisNexisValues SSN = new LexisNexisValues(0, false, "SSN", false);
        public static readonly LexisNexisValues PolNo = new LexisNexisValues(1, false, "Policy No", false);
        public static readonly LexisNexisValues FirstName = new LexisNexisValues(2, false, "First Name");
        public static readonly LexisNexisValues MiddleInitial = new LexisNexisValues(3, false, "Middle Initial");
        public static readonly LexisNexisValues LastName = new LexisNexisValues(4, false, "Last Name");
        public static readonly LexisNexisValues NameSuffix = new LexisNexisValues(5, false, "Name Suffix");
        public static readonly LexisNexisValues DOB = new LexisNexisValues(6, false, "Date Of Birth");
        public static readonly LexisNexisValues StreetAddress1 = new LexisNexisValues(7, false, "Street Address1");
        public static readonly LexisNexisValues StreetAddress2 = new LexisNexisValues(8, false, "Street Address2");
        public static readonly LexisNexisValues StreetAddress3 = new LexisNexisValues(9, false, "Street Address3");
        public static readonly LexisNexisValues StreetAddress4 = new LexisNexisValues(10, false, "Street Address4");
        public static readonly LexisNexisValues City = new LexisNexisValues(11, false, "City");
        public static readonly LexisNexisValues State = new LexisNexisValues(12, false, "State");
        public static readonly LexisNexisValues Zip = new LexisNexisValues(13, false, "Zip Code");
        public static readonly LexisNexisValues PolicyStatus = new LexisNexisValues(14, false, "Policy Status", false);

        public static readonly LexisNexisValues ServicingAgentNumber =
            new LexisNexisValues(15, true, "Servicing Agent Number", false);

        public static readonly LexisNexisValues PolicyIssueDate = new LexisNexisValues(16, false, "Policy Issue Date", false);

        public static readonly LexisNexisValues PolicyTerminationDate =
            new LexisNexisValues(17, false, "Policy Termination Date", false);

        private LexisNexisValues(int idxValue, bool intable, string name, bool ignore = true)
        {
            IdxValue = idxValue;
            Intable = intable;
            Name = name;
            ToIgnore = ignore;
        }

        public int IdxValue { get; }
        public bool Intable { get; }
        public string Name { get; }
        public bool ToIgnore { get; }

        public static IEnumerable<LexisNexisValues> Values
        {
            get
            {
                yield return SSN;
                yield return PolNo;
                yield return FirstName;
                yield return MiddleInitial;
                yield return LastName;
                yield return NameSuffix;
                yield return DOB;
                yield return StreetAddress1;
                yield return StreetAddress2;
                yield return StreetAddress3;
                yield return StreetAddress4;
                yield return City;
                yield return State;
                yield return Zip;
                yield return PolicyStatus;
                yield return ServicingAgentNumber;
                yield return PolicyIssueDate;
                yield return PolicyTerminationDate;
            }
        }

        public static LexisNexisValues GetValue(int key) => Values.First(v => v.IdxValue == key);
    }
}
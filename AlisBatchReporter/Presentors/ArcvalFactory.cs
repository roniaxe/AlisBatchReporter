using System.Collections.Generic;

namespace AlisBatchReporter.Presentors
{
    internal class ArcvalFactory
    {
        private static readonly Dictionary<string, bool> InActivePolicies = new Dictionary<string, bool>();
        public static readonly Dictionary<string, ArcvalRowStatus?> PolicyStatusDic = new Dictionary<string, ArcvalRowStatus?>();
        public static ArcvalInstance GetArcvalInstance(string arcvalRow)
        {
            ArcvalInstance arcval = new ArcvalInstance(arcvalRow);
            if (!arcval.Validate()) return null;
            arcval.SetPolicyNo();
            if (InActivePolicies.ContainsKey(arcval.PolicyNo)) return null;
            arcval.Configure();
            if (arcval.Status == ArcvalRowStatus.Inactive)
            {
                InActivePolicies.Add(arcval.PolicyNo, true);
                return null;
            }
            arcval.Process();
            return arcval;
        }
    }
}
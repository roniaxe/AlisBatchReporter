using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

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

    internal class ArcvalUtils
    {
        public static string FormatDifferenceString(ArcvalInstance src, ArcvalInstance ob, int? idx)
        {
            string extra = null;
            if (ob != null && idx != null)
            {
                extra = $@" - Source Val: {src.ArcvalProps[idx.Value].Value}, Outbound Val: {ob.ArcvalProps[idx.Value].Value}";
            }

            return $@"{src.Key} ({src.Status.ToString()}) {extra}";
        }
    }
}
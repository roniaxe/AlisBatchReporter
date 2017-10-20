using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlisBatchReporter.Classes
{
    class ArcvalValuesType1 : Values
    {
        #region Constructor
        public ArcvalValuesType1(int idxValue, string name, bool ignore = false, bool toRound = false) : base(idxValue, name, ignore, toRound)
        {
        }
        #endregion

        #region StaticProperties
        public static readonly Values StatusCode = new ArcvalValuesType1(11, "CON-STATUS");
        public static readonly Values State = new ArcvalValuesType1(12, "STATE-CODE");
        #endregion
    }
}

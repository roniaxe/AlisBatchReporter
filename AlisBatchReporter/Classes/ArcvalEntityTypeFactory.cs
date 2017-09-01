using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlisBatchReporter.Classes
{
    public class ArcvalEntityTypeFactory
    {
        public static Values GetArcvalType(string type)
        {
            Values result = null;
            switch (type)
            {
                case "Type1":
                    result = new ArcvalValues();
                    break;
                case "Type7":
                    result = new ArcvalValuesType7();
                    break;
                case "Type6A":
                    result = new ArcvalValuesType6A();
                    break;
                case "Type5":
                    result = new ArcvalValuesType5();
                    break;
                default:
                    break;
            }
            return result;
        }
    }
}

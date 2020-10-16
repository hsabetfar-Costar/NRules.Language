using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NRules.RuleSharp
{
    internal static class ExtensionMethods
    {
        public static Type GetNullabeType(this Type t)
        {
             return typeof(Nullable<>).MakeGenericType(t);
        }
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SparrowCMS.Base
{
    public static class AssemblyHelper
    {
        private static readonly string defaultNamespace;
        private static readonly string pluginNamespace;
        static AssemblyHelper()
        {
            defaultNamespace = "SparrowCMS.Base";
            pluginNamespace = "SparrowCMS.Plugin";
        }


        public static Type GetType(string typeName, string className)
        {
            return Cache<Type>.GetOrSet(typeName + "." + className, () =>
            {
                return Type.GetType(string.Format("{0}.{1}.{2}", defaultNamespace, typeName, className))
                    ?? Type.GetType(string.Format("{0}.{1}.{2}", pluginNamespace, typeName, className));
            });
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SparrowCMS.Base
{
    public static class AssemblyHelper
    {

        public static Type GetType(string[] classNames)
        {
            Type type = null;
            foreach (var className in classNames)
            {
                type = Type.GetType(className, false, true);
                if (type != null) break;
            }
            return type;
        }

        public static T CreateInstance<T>(Type type, T defaultT = null) where T : class
        {
            if (type == null) return defaultT;

            return Cache<T>.GetOrSet(type.FullName, () => (T)Activator.CreateInstance(type));
        }
    }
}

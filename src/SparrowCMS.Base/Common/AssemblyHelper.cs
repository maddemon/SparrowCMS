using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SparrowCMS.Base
{
    public static class AssemblyHelper
    {
        private static ConcurrentDictionary<string, Type> _types = new ConcurrentDictionary<string, Type>();

        static AssemblyHelper()
        {
            foreach (var ass in AppDomain.CurrentDomain.GetAssemblies())
            {
                if (!ass.FullName.StartsWith("Sparrow"))
                {
                    continue;
                }
                foreach (var type in ass.GetTypes())
                {
                    if (!_types.ContainsKey(type.FullName))
                        _types.TryAdd(type.FullName.ToLower(), type);
                }
            }
        }

        public static Type GetType(string[] classNames)
        {
            Type type = null;
            foreach (var className in classNames)
            {
                if(_types.ContainsKey(className.ToLower()))
                {
                    return _types[className.ToLower()];
                }
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

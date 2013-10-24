using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
                AddAssembly(ass);
            }
        }

        public static void AddAssembly(Assembly assembly)
        {
            if (assembly == null || !assembly.FullName.StartsWith("Sparrow"))
            {
                return;
            }

            foreach (var type in assembly.GetTypes())
            {
                if (!_types.ContainsKey(type.FullName))
                {
                    _types.TryAdd(type.FullName.ToLower(), type);
                }
            }
        }

        public static Type GetType(string[] classNames)
        {
            foreach (var className in classNames)
            {
                if (_types.ContainsKey(className.ToLower()))
                {
                    return _types[className.ToLower()];
                }
            }
            return null;
        }

        public static T CreateInstance<T>(Type type, T defaultT = null) where T : class
        {
            if (type == null)
            {
                return defaultT;
            }
            return Cache<T>.GetOrSet(type.FullName, () => (T)Activator.CreateInstance(type));
        }
    }
}

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SparrowCMS.Base
{
    public class Factory
    {
        private static Factory Instance = new Factory();
        private ConcurrentDictionary<string, List<Type>> AllTypes;

        private Factory()
        {
            InitTypes();
        }

        private void InitTypes()
        {
            AllTypes = new ConcurrentDictionary<string, List<Type>>();
            AllTypes.TryAdd("Shared", new List<Type>());
            AllTypes.TryAdd("Others", new List<Type>());

            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                if (assembly.FullName.StartsWith("Sparrow"))
                {
                    AddAssembly(assembly);
                }
            }
        }

        public void AddAssembly(Assembly assembly)
        {
            if (assembly == null)
            {
                return;
            }

            foreach (var type in assembly.GetTypes())
            {
                var key = type.FullName;
                var attr = type.GetCustomAttributes(true).FirstOrDefault(a => a is LabelAttribute);
                if (attr != null)
                {
                    key = ((LabelAttribute)attr).Name;
                    if (!AllTypes.ContainsKey(key))
                    {
                        AllTypes.TryAdd(key, new List<Type>());
                    }
                    AllTypes[key].Add(type);
                }
                else
                {
                    foreach (var kv in AllTypes)
                    {
                        if (type.FullName.Contains(kv.Key))
                        {
                            AllTypes[kv.Key].Add(type);
                        }
                        else
                        {
                            AllTypes["Others"].Add(type);
                        }
                    }
                }
            }
        }

        private Type SearchType(string labelName, string typeName)
        {
            Type result = null;

            if (AllTypes.ContainsKey(labelName))
            {
                //寻找包含该typeName的所有类型
                var types = AllTypes[labelName].Where(t => t.FullName.ToLower().Contains(typeName.ToLower()));
                //寻找插件下的非共享类
                result = types.FirstOrDefault(t => t.FullName.Contains("Plugin") && !t.FullName.Contains("Shared"));
                if (result != null) return result;
                //寻找插件下的共享类
                result = types.FirstOrDefault(t => t.FullName.Contains("Plugin") && t.FullName.Contains("Shared"));
                if (result != null) return result;
                //寻找系统Base的非共享类
                result = types.FirstOrDefault(t => !t.FullName.Contains("Shared"));
                if (result != null) return result;
                //寻找Base的共享类
                result = types.FirstOrDefault(t => t.FullName.Contains("Shared"));
                if (result != null) return result;
            }

            //寻找Base的共享类
            result = FindType(AllTypes["Shared"], labelName, typeName);
            if (result != null) return result;
            //寻找未知的归属类
            result = FindType(AllTypes["Others"], labelName, typeName);
            if (result != null) return result;
            return null;
        }

        private Type FindType(IEnumerable<Type> types, string labelName, string typeName)
        {
            var names = labelName.ToLower().Split('.');
            foreach (var type in types)
            {
                var hasContains = true;
                foreach (var name in type.FullName.ToLower().Split('.'))
                {
                    if (!names.Contains(name))
                    {
                        hasContains = false;
                    }
                }

                if (hasContains && type.FullName.ToLower().Contains(typeName.ToLower()))
                {
                    return type;
                }
            }

            return null;
        }

        private T CreateInstance<T>(string labelName, string typeName)
        {
            var type = SearchType(labelName, typeName);
            if (type == null)
            {
                return default(T);
            }

            return (T)Activator.CreateInstance(type);
        }

        public static T GetInstance<T>(string labelName, string typeName = "Default")
        {
            return Instance.CreateInstance<T>(labelName, typeName);
        }
    }
}

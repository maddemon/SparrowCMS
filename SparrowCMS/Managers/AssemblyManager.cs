using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using SparrowCMS.Models;

namespace SparrowCMS.Managers
{
    internal class AssemblyManager
    {
        private static List<TypeDescriptor> _descriptors = new List<TypeDescriptor>();

        public class TypeDescriptor
        {
            public Type ClassType { get; set; }

            public string AliasName { get; set; }

            public string Plugin { get; set; }

            private static Type[] _cmsBaseTypes = new[] 
            {
                typeof(ILabel),
                typeof(IField),
                typeof(IApi),
                typeof(ILabelParameterFunction),
                typeof(FieldFunction)
            };

            public static TypeDescriptor Create(Type type)
            {
                var result = new TypeDescriptor { ClassType = type };

                var attr = type.GetCustomAttributes(true).FirstOrDefault(a => a is NameAttribute);
                if (attr != null)
                {
                    result.AliasName = ((NameAttribute)attr).Name;
                    return result;
                }

                foreach (var baseType in _cmsBaseTypes)
                {
                    if (baseType.IsAssignableFrom(type))
                    {
                        return result;
                    }
                }
                return null;
            }
        }

        public AssemblyManager()
        {
            if (_descriptors.Count == 0)
            {
                //默认把Sparrow前缀的dll添加
                foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies().Where(assembly => assembly.FullName.StartsWith("Sparrow")))
                {
                    AddAssembly(assembly);
                }
            }
        }

        private void AddAssembly(Assembly assembly)
        {
            if (assembly == null)
            {
                return;
            }

            foreach (var type in assembly.GetTypes())
            {
                var desc = TypeDescriptor.Create(type);
                if (desc != null)
                {
                    _descriptors.Add(desc);
                }
            }
        }

        public void LoadDll(Plugin plugin)
        {
            var pluginPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Plugins", plugin.EnName);
            var dirs = Directory.GetDirectories(pluginPath);
            if (dirs.Contains("bin"))
            {
                pluginPath = Path.Combine(pluginPath, "bin");
            }
            foreach (var fileName in Directory.GetFiles(pluginPath))
            {
                if (fileName.EndsWith("dll"))
                {
                    var filePath = Path.Combine(pluginPath, fileName);
                    var assembly = Assembly.LoadFrom(filePath);
                    AddAssembly(assembly);
                }
            }
        }

        private Type Search(string fullName)
        {
            foreach (var desc in _descriptors)
            {
                if (!string.IsNullOrEmpty(desc.AliasName) && desc.AliasName.ToLower() == fullName.ToLower())
                {
                    return desc.ClassType;
                }
                if (desc.ClassType.FullName.ToLower() == fullName.ToLower())
                {
                    return desc.ClassType;
                }
            }
            return null;
        }

        //public T CreateInstance<T>(string partialName)
        //{

        //    var descs = _descriptors.Where(d => typeof(T).IsAssignableFrom(d.ClassType) && (
        //        d.AliasName == partialName
        //    ||
        //        d.ClassType.FullName.EndsWith(partialName)
        //        ||
        //        d.ClassType.FullName.EndsWith(partialName + ".Default")
        //        ));
        //    var desc = descs.FirstOrDefault(d => d.AliasName == partialName) ?? descs.FirstOrDefault();
        //    if (desc == null)
        //    {
        //        return default(T);
        //    }

        //    return (T)Activator.CreateInstance(desc.ClassType);
        //}

        internal T CreateInstance<T>(string[] fullNames)
        {
            foreach (var fullName in fullNames)
            {
                var type = Search(fullName);
                if (type != null && typeof(T).IsAssignableFrom(type))
                {
                    return (T)Activator.CreateInstance(type);
                }
            }
            return default(T);
        }

    }
}

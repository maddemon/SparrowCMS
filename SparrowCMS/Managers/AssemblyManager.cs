using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using SparrowCMS.Models;

namespace SparrowCMS.Managers
{
    public class AssemblyManager : ManagerBase
    {

        private List<ClassDescriptor> _descriptors = new List<ClassDescriptor>();

        public class ClassDescriptor
        {
            public Type ClassType { get; set; }

            public string AliasName { get; set; }

            public string Plugin { get; set; }

            //private static string[] LimitNames = new[] { "Apis", "Labels", "Fields", "Functions", "Parameters" };
            public static ClassDescriptor Create(Type type)
            {
                var result = new ClassDescriptor { ClassType = type };

                var attr = type.GetCustomAttributes(true).FirstOrDefault(a => a is NameAttribute);
                if (attr != null)
                {
                    result.AliasName = ((NameAttribute)attr).Name;
                    return result;
                }

                //foreach (var name in LimitNames)
                //{
                //    if (type.FullName.Contains(name))
                //    {
                //        return result;
                //    }
                //}
                return result;
            }
        }

        public AssemblyManager()
        {
            //默认把Sparrow前缀的dll添加
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies().Where(assembly => assembly.FullName.StartsWith("Sparrow")))
            {
                AddAssembly(assembly);
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
                var desc = ClassDescriptor.Create(type);
                if (desc != null)
                {
                    _descriptors.Add(desc);
                }
            }
        }

        public void LoadDll(Plugin plugin)
        {
            var pluginPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, plugin.EnName);
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
                    var assembly = Assembly.LoadFile(filePath);
                    AddAssembly(assembly);
                }
            }
        }

        public Type Search(string fullName)
        {
            foreach (var desc in _descriptors)
            {
                if (desc.AliasName.ToLower() == fullName.ToLower())
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
    }
}

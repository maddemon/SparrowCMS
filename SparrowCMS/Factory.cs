using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SparrowCMS
{

    public class Factory
    {

        internal enum ClassType
        {
            Unknown,
            Label,
            Parameter,
            Function,
            Field,
            Api
        }

        internal class ClassSearchHelper
        {
            public static ClassType GetSearhType(string className)
            {
                foreach (var name in Enum.GetNames(typeof(ClassType)))
                {
                    if (className.Contains(name))
                    {
                        return (ClassType)Enum.Parse(typeof(ClassType), name);
                    }
                }
                return ClassType.Unknown;
            }

            public static string[] GetNamespaces(ClassType classType, string labelName, string className)
            {
                var pluginName = labelName;
                var dotIndex = labelName.IndexOf('.');
                if (dotIndex > -1)
                {
                    pluginName = labelName.Substring(0, dotIndex);
                }
                dotIndex = labelName.LastIndexOf('.');
                var parentName = labelName;
                if (labelName.Contains(className))
                {
                    parentName = labelName.Substring(0, dotIndex);
                }
                switch (classType)
                {
                    case ClassType.Unknown:
                        return null;
                    case ClassType.Api:
                        return new string[]
                        {
                            string.Format("{0}.Apis.{1}",pluginName , className),
                            string.Format("{0}.Shared.Apis.{1}",parentName , className),
                            string.Format("SparrowCMS.Apis.{0}",parentName),
                            string.Format("SparrowCMS.Apis.{0}.{1}",parentName,className),
                        };
                }
                if (classType == ClassType.Unknown) return null;
                if (classType == ClassType.Api)
                {
                    return new List<string> 
                { 
                    string.Format("{0}.Apis.{1}",parentName,className),
                    string.Format("SparrowCMS.Apis.{0}",className)
                };
                }

                return new string[]
                {
                    string.Format("{0}.{2}s.{1}",parentName , className),
                    string.Format("{0}.Shared.{2}s.{1}",parentName , className),
                    string.Format("SparrowCMS.{2}s.{0}",parentName),
                    string.Format("SparrowCMS.{2}s.{0}.{1}",parentName,className),
                    string.Format("SparrowCMS.Shared.{2}s.{0}",parentName),
                    string.Format("SparrowCMS.Shared.{2}s.{0}.{1}",parentName,className)
                };

            }
        }

        internal class ClassDescription
        {
            public ClassDescription()
            {
            }

            public Type Type { get; set; }

            public string AliasName { get; set; }

            private static string[] _classTypes;
            static ClassDescription()
            {
                _classTypes = Enum.GetNames(typeof(ClassType));
            }

            internal static ClassDescription Create(Type type)
            {
                var result = new ClassDescription { Type = type };

                var attr = type.GetCustomAttributes(true).FirstOrDefault(a => a is NameAttribute);
                if (attr != null)
                {
                    result.AliasName = ((NameAttribute)attr).Name;
                    return result;
                }
                foreach (var name in _classTypes)
                {
                    if (type.FullName.Contains(name + "s"))
                    {
                        return result;
                    }
                }
                return null;
            }
        }

        public static Factory Instance = new Factory();

        private readonly List<ClassDescription> _allTypes = new List<ClassDescription>();

        private Factory()
        {
            InitTypes();
        }

        private void InitTypes()
        {
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies().Where(assembly => assembly.FullName.StartsWith("Sparrow")))
            {
                AddAssembly(assembly);
            }
        }

        /// <summary>
        /// 类名字空间约束 & 查询规则
        /// ·插件的Label类可以不放在labels文件夹下，但其他Field、Parameter、Function和Attribute必须放在对应的文件夹下。
        /// ·公共的其他类放在Shared文件夹下
        /// ·插件DLL名称最好包含Label名称的一部分或其前缀
        /// 
        /// ·优先查询插件dll
        /// ·优先查询非共享类
        /// 
        /// 插件之间的类名要避免冲突，特别是别名！
        /// </summary>
        /// <param name="assembly"></param>
        public void AddAssembly(Assembly assembly)
        {
            if (assembly == null)
            {
                return;
            }

            var classTypes = Enum.GetNames(typeof(ClassType));
            foreach (var type in assembly.GetTypes())
            {
                var desc = ClassDescription.Create(type);
                if (desc != null)
                {
                    _allTypes.Add(desc);
                }
            }
        }

        private ClassDescription GetClassDescription(string parentName, string className, ClassType classType)
        {
            if (classType == ClassType.Unknown) return null;

            var typeNamespaces = ClassSearchHelper.GetNamespaces(classType, parentName, className);

            foreach (var ns in typeNamespaces)
            {
                var searchType = _allTypes.FirstOrDefault(t => (t.AliasName != null && t.AliasName.ToLower() == ns.ToLower()) || t.Type.FullName.ToLower().Contains(ns.ToLower()));
                if (searchType != null) return searchType;
            }
            return null;
        }

        public T GetInstance<T>(string parentName, string className = "Default")
        {
            var searchType = ClassSearchHelper.GetSearhType(typeof(T).FullName);

            var desc = GetClassDescription(parentName, className, searchType);

            if (desc == null)
            {
                return default(T);
            }

            return (T)Activator.CreateInstance(desc.Type);
        }
    }
}

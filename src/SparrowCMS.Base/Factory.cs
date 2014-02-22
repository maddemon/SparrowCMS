using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SparrowCMS.Core
{
    public enum ClassType
    {
        Parameter,
        Function,
        Attribute,
        Unknown,
        Label,
        Field,
        Api
    }

    public static class SearchHelper
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

        public static IEnumerable<string> GetNamespaces(this ClassType classType, string parentName, string className)
        {
            if (classType == ClassType.Unknown) return null;
            if (classType == ClassType.Api)
            {
                return new List<string> { 
                 string.Format("{0}.Apis.{1}",parentName,className),
                 string.Format("Core.Apis.{0}",className)
                };
            }

            return new List<string>
            {
                string.Format("{0}.{1}",parentName , className),
                string.Format("{0}.{1}s.{2}",parentName ,classType , className),
                string.Format("{0}.Shared.{1}s.{2}",parentName , classType , className),
                string.Format("Core.Labels.{0}.{1}", parentName, className),
                string.Format("Core.Labels.{0}.{1}s.{2}" , parentName , classType , className),
                string.Format("Core.Labels.Shared.{0}s.{1}",classType , className),
                string.Format("Core.Apis.{1}",classType,className)
            };

        }
    }

    internal class ClassDescription
    {
        public ClassDescription(Type type)
        {
            Type = type;

            var attr = type.GetCustomAttributes(true).FirstOrDefault(a => a is LabelNameAttribute);
            if (attr != null)
            {
                AliasName = ((LabelNameAttribute)attr).Name;

            }
        }

        public Type Type { get; set; }

        public string AliasName { get; set; }

    }

    public class Factory
    {

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

            foreach (var type in assembly.GetTypes())
            {
                _allTypes.Add(new ClassDescription(type));
            }
        }

        private ClassDescription GetClassDescription(string parentName, string className, ClassType classType)
        {
            if (classType == ClassType.Unknown) return null;

            var typeNamespaces = classType.GetNamespaces(parentName, className);

            foreach (var ns in typeNamespaces)
            {
                var searchType = _allTypes.FirstOrDefault(t => (t.AliasName != null && t.AliasName.ToLower() == ns.ToLower()) || t.Type.FullName.ToLower().Contains(ns.ToLower()));
                if (searchType != null) return searchType;
            }
            return null;
        }

        public T GetInstance<T>(string parentName, string className = "Default")
        {
            var searchType = SearchHelper.GetSearhType(typeof(T).FullName);

            var desc = GetClassDescription(parentName, className, searchType);

            if (desc == null)
            {
                return default(T);
            }

            return (T)Activator.CreateInstance(desc.Type);
        }
    }
}

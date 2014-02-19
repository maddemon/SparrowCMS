using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SparrowCMS.Core
{
    internal enum ClassType
    {
        Parameter,
        Function,
        Attribute,
        Unknown,
        Label,
        Field,
    }

    internal class SearchType
    {
        public SearchType(Type type)
        {
            Type = type;

            ClassType = GetClassType(type.FullName);

            var attr = type.GetCustomAttributes(true).FirstOrDefault(a => a is LabelNameAttribute);
            if (attr != null)
            {
                AliasName = ((LabelNameAttribute)attr).Name;

            }
        }

        public static ClassType GetClassType(string typeName)
        {
            foreach (var name in Enum.GetNames(typeof(ClassType)))
            {
                if (typeName.Contains(name))
                {
                    return (ClassType)Enum.Parse(typeof(ClassType), name);
                }
            }
            return ClassType.Unknown;
        }

        public Type Type { get; set; }

        public ClassType ClassType { get; set; }

        public string AliasName { get; set; }

    }

    public class Factory
    {

        public static Factory Instance = new Factory();
        private readonly List<SearchType> _allTypes = new List<SearchType>();

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
                _allTypes.Add(new SearchType(type));
            }
        }

        private Type Search(string labelName, string typeName, ClassType classType)
        {
            if (classType == ClassType.Unknown) return null;

            var typeNamespaces = new List<string>
            {
                string.Format("{0}.{1}",labelName , typeName),
                string.Format("{0}.{1}s.{2}",labelName ,classType , typeName),
                string.Format("{0}.Shared.{1}s.{2}",labelName , classType , typeName),
                string.Format("Core.Labels.{0}.{1}", labelName, typeName),
                string.Format("Core.Labels.{0}.{1}s.{2}" , labelName , classType , typeName),
                string.Format("Core.Labels.Shared.{0}s.{1}",classType , typeName),
            };

            if (classType == ClassType.Label) typeNamespaces.Add(labelName);

            foreach (var ns in typeNamespaces)
            {
                var searchType = _allTypes.FirstOrDefault(t => t.ClassType == classType && ((t.AliasName != null && t.AliasName.ToLower() == ns.ToLower()) || t.Type.FullName.ToLower().Contains(ns.ToLower())));
                if (searchType != null) return searchType.Type;
            }
            return null;
        }

        public T GetInstance<T>(string labelName, string typeName = "Default")
        {
            var classType = SearchType.GetClassType(typeof(T).FullName);
            var type = Search(labelName, typeName, classType);

            if (type == null)
            {
                return default(T);
            }

            return (T)Activator.CreateInstance(type);
        }
    }
}

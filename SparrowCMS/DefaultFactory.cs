using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SparrowCMS.Managers;

namespace SparrowCMS
{
    public interface IFactory
    {
        T CreateInstance<T>(string fullName);
    }

    /// <summary>
    /// 搜索类型,顺序不能更改
    /// </summary>
    public enum SearchType
    {
        Function, Parameter, Field, Label, Api, Unknown
    }

    public class DefaultFactory : IFactory
    {
        private ManagerCore Core = ManagerCore.Instance;

        protected virtual string[] GetFullNames(string fullName, SearchType searchType)
        {
            var parentName = fullName.Split('.')[0];
            var className = fullName.Split('.')[1];
            var searchName = string.Empty;
            switch (searchType)
            {
                case SearchType.Api:
                case SearchType.Label:
                    searchName = searchType.ToString();
                    break;
                case SearchType.Field:
                case SearchType.Function:
                case SearchType.Parameter:
                    searchName = "Labels." + searchType.ToString();
                    break;
                default:
                    return null;
            }
            return new string[]
            {
                string.Format("{0}.{2}s.{1}",parentName , className , searchName),
                string.Format("{0}.Shared.{2}s.{1}",parentName , className, searchName),
                string.Format("SparrowCMS.{1}s.{0}",parentName, searchName),
                string.Format("SparrowCMS.{2}s.{0}.{1}",parentName,className, searchName),
                string.Format("SparrowCMS.Shared.{1}s.{0}",parentName, searchName),
                string.Format("SparrowCMS.Shared.{2}s.{0}.{1}",parentName,className, searchName)
            };
        }

        private static SearchType GetSearchType(Type type)
        {
            foreach (var name in Enum.GetNames(typeof(SearchType)))
            {
                if (type.Name.Contains(name))
                {
                    return (SearchType)Enum.Parse(typeof(SearchType), name);
                }
            }
            return SearchType.Unknown;
        }

        public T CreateInstance<T>(string classFullName)
        {
            var searchType = GetSearchType(typeof(T));
            var fullNames = GetFullNames(classFullName, searchType);
            foreach (var fullName in fullNames)
            {
                var type = Search(fullName);
                if (type != null)
                {
                    return Activator.CreateInstance<T>();
                }
            }
            return default(T);
        }
    }
}

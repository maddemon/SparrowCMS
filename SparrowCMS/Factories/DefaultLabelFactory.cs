using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SparrowCMS.Managers;

namespace SparrowCMS.Factories
{
    public interface ILabelFactory
    {
        T CreateInstance<T>(string labelName, string className);
    }

    public class DefaultLabelFactory : ILabelFactory
    {
        private enum SearchType
        {
            Function, Parameter, Field, Label, Api, Unknown
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

        private string[] GetFullNames(string labelText, string className, SearchType type)
        {
            var pluginName = string.Empty;
            var labelName = string.Empty;
            var dotIndex = labelText.LastIndexOf('.');
            if (dotIndex > -1)
            {
                pluginName = labelText.Substring(0, dotIndex);
                labelName = labelText.Substring(dotIndex + 1);
            }
            else
            {
                pluginName = labelText;
                labelName = "Default";
            }
            switch (type)
            {
                case SearchType.Label:
                    return GetLabelFullNames(pluginName, labelName);
                case SearchType.Function:
                    return GetFunctionFullNames(pluginName, labelName, className);
                case SearchType.Field:
                    return GetFieldFullNames(pluginName, labelName, className);
                default:
                    return null;
            }
        }

        protected virtual string[] GetLabelFullNames(string pluginName, string labelName)
        {
            if (labelName == "Default")
            {
                return new string[]
                { 
                    string.Format("SparrowCMS.Labels.{0}" , pluginName),
                    string.Format("SparrowCMS.Labels.{0}.{1}" , pluginName, labelName)
                };
            }
            else
            {
                return new string[]
                { 
                    string.Format("{0}.Labels.{1}",pluginName , labelName),
                    string.Format("{0}.Labels.{1}.Default",pluginName , labelName),
                    string.Format("{0}.Shared.Labels.{1}",pluginName,labelName),
                    string.Format("{0}.Shared.Labels.{1}.Default",pluginName,labelName),
                    string.Format("SparrowCMS.Labels.{0}.{1}",pluginName,labelName)
                };
            }
        }

        protected virtual string[] GetFieldFullNames(string pluginName, string labelName, string fieldName)
        {
            return new string[]
            { 
                string.Format("{0}.Labels.{1}.Fields.{2}",pluginName,labelName,fieldName ),
                string.Format("{0}.Shared.Fields.{1}",pluginName,fieldName ),
                string.Format("SparrowCMS.Labels.{0}.{1}.Fields.{2}",pluginName,labelName,fieldName),
                string.Format("SparrowCMS.Labels.Shared.Fields.{0}",fieldName),
            };
        }

        protected virtual string[] GetFunctionFullNames(string pluginName, string labelName, string functionName)
        {
            return new string[]
            { 
                string.Format("{0}.Labels.{1}.Functions.{2}",pluginName,labelName,functionName ),
                string.Format("{0}.Shared.Functions.{1}",pluginName,functionName ),
                string.Format("SparrowCMS.Labels.{0}.{1}.Functions.{2}",pluginName,labelName,functionName),
                string.Format("SparrowCMS.Labels.Shared.Functions.{0}",functionName),
            };
        }

        private static readonly AssemblyManager AssemblyManager = new AssemblyManager();
        public virtual T CreateInstance<T>(string labelName, string className)
        {
            var searchType = GetSearchType(typeof(T));
            var fullNames = GetFullNames(labelName, className, searchType);
            return AssemblyManager.CreateInstance<T>(fullNames);
        }
    }
}

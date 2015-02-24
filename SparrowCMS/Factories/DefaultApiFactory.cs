using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SparrowCMS.Managers;

namespace SparrowCMS.Factories
{
    public interface IApiFactory
    {
        IApi CreateInstance(string pluginName, string apiName);
    }

    public class DefaultApiFactory : IApiFactory
    {
        protected virtual string[] GetFullNames(string pluginName, string apiName)
        {
            return new string[]
            { 
                string.Format("{0}.Apis.{1}",pluginName , apiName),
                string.Format("{0}.Shared.Apis.{1}",pluginName , apiName),
                string.Format("SparrowCMS.Apis.{0}",apiName),
                string.Format("SparrowCMS.Apis.{0}.{1}",pluginName,apiName)
            };
        }

        private static readonly AssemblyManager AssemblyManager = new AssemblyManager();

        public IApi CreateInstance(string pluginName, string apiName)
        {
            return AssemblyManager.CreateInstance<IApi>(GetFullNames(pluginName, apiName));
        }
    }
}

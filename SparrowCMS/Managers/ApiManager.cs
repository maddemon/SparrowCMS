using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SparrowCMS.Factories;

namespace SparrowCMS.Managers
{
    public class ApiManager
    {
        private List<IApiFactory> GetApiFactories()
        {
            //TODO
            return new List<IApiFactory>() { new DefaultApiFactory() };
        }

        public IApi CreateApi(string pluginName, string apiName)
        {
            var factories = GetApiFactories();
            foreach (var factory in factories)
            {
                var result = factory.CreateInstance(pluginName, apiName);
                if (result != null)
                {
                    return result;
                }
            }
            return null;
        }

        private static readonly Dictionary<string, ApiDescriptor> Descriptors = new Dictionary<string, ApiDescriptor>();

        //private ApiDescriptor Create(IApi apiInstance)
        //{
        //    throw new NotImplementedException();
        //}

        internal ApiDescriptor GetApi(string pluginName, string apiName)
        {
            var key = (pluginName ?? "SparrowCMS") + "." + apiName;
            if (Descriptors.ContainsKey(key))
            {
                return Descriptors[key];
            }

            foreach (var factory in GetApiFactories())
            {
                var apiInstance = factory.CreateInstance(pluginName, apiName);
                if (apiInstance != null)
                {
                    var desc = ApiDescriptor.Create(apiInstance);
                    Descriptors.Add(key, desc);
                    return desc;
                }
            }
            return null;
        }
    }
}

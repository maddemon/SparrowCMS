using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SparrowCMS.Managers
{
    public class FactoryManager : ManagerBase
    {
        private const string DEFAULT_KEY = "Default";

        private static readonly Dictionary<string, IFactory> Data = new Dictionary<string, IFactory>();

        public FactoryManager()
        {
            AddFactory(DEFAULT_KEY, new DefaultFactory());
        }

        public void AddFactory(string pluginName, IFactory factory)
        {
            if (Data.ContainsKey(pluginName))
            {
                Data[pluginName] = factory;
            }
            else
            {
                Data.Add(pluginName, factory);
            }
        }

        public IFactory GetFactory(string pluginName = DEFAULT_KEY)
        {
            return Data.ContainsKey(pluginName) ? Data[pluginName] : Data[DEFAULT_KEY];
        }
    }
}

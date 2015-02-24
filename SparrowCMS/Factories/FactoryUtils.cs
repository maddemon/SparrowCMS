using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SparrowCMS.Managers;

namespace SparrowCMS
{
    public class FactoryUtils
    {
        private static FactoryManager FactoryManager = new FactoryManager();

        public static T CreateInstance<T>(string labelName, string className)
        {
            if (typeof(T) is IApi)
            {
                var factories = FactoryManager.GetApiFactories();
                foreach (var factory in factories)
                {
                    var result = factory.CreateInstance(labelName, className);
                    if (result != null)
                    {
                        return (T)result;
                    }
                }
            }
            else
            {

                var factories = FactoryManager.GetLabelFactories();
                foreach (var factory in factories)
                {
                    var result = factory.CreateInstance<T>(labelName, className);
                    if (result != null)
                    {
                        return result;
                    }
                }
            }
            return default(T);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SparrowCMS.Managers;

namespace SparrowCMS.Factories
{
    public interface IFunctionFactory
    {
        T CreateFunction<T>(string pluginName, string labelName, string functionName);
    }

    public class DefaultFunctionFactory : IFunctionFactory
    {
        protected static AssemblyManager AssemblyManager = new AssemblyManager();

        protected virtual string[] GetFullNames(string pluginName, string labelName, string functionName)
        {
            return LabelFactoryHelper.GetFullNames(pluginName, labelName, functionName, "Function");
        }

        public T CreateFunction<T>(string pluginName, string labelName, string functionName)
        {
            return AssemblyManager.CreateInstance<T>(GetFullNames(pluginName, labelName, functionName));
        }
    }
}

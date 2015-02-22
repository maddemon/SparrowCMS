using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using SparrowCMS.Managers;

namespace SparrowCMS.Parsers
{
    public class LabelParameterFunctionParser
    {
        private static readonly Regex ParameterFunctionRegex = new Regex(@"(?<name>[^)(]+)\([^)(]+\)", RegexOptions.Compiled);

        private FactoryManager FactoryManager = FactoryManager.GetInstance();

        public static IParameterFunction Parse(LabelDescriptor descriptor, string parameterRawValue)
        {
            var match = ParameterFunctionRegex.Match(parameterRawValue);
            if (match == null || match.Groups.Count <= 1) return null;
            var factories = FactoryManager.GetInstance().GetFunctionFactories();
            var name = match.Groups["name"].Value;
            foreach (var factory in factories)
            {
                var func = factory.CreateFunction<IParameterFunction>(descriptor.PluginName, descriptor.LabelName, name);
                if (func != null)
                {
                    return func;
                }
            }
            return null;
        }

    }
}

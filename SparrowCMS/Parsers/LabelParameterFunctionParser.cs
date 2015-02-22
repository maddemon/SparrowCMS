using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SparrowCMS.Parsers
{
    public class LabelParameterFunctionParser
    {
        private static readonly Regex ParameterFunctionRegex = new Regex(@"(?<name>[^)(]+)\([^)(]+\)", RegexOptions.Compiled);

        public static IParameterFunction Parse(LabelDescriptor descriptor, string parameterRawValue)
        {
            var match = ParameterFunctionRegex.Match(parameterRawValue);
            if (match == null || match.Groups.Count <= 1) return null;
            return CMSContext.Current.GetFactory(descriptor.PluginName).CreateInstance<IParameterFunction>(descriptor.LabelName + ".Functions." + match.Groups["name"].Value);
        }

    }
}

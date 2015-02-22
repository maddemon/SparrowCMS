using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace SparrowCMS.Parsers
{
    public class LabelParameterParser
    {
        private static readonly Regex ParameterRegex = new Regex(@"(?<name>\w+)\s*=\s*(?<value>""[^""]+""|[^\s\/]+|'[^']+')", RegexOptions.Compiled);

        private static LabelParameter Parse(LabelDescriptor descriptor, Match match)
        {
            var name = match.Groups["name"].Value;
            var value = match.Groups["value"].Value.Trim('"').Trim('\'');

            var labelParameter = new LabelParameter { Name = name, RawValue = value };

            labelParameter.Function = LabelParameterFunctionParser.Parse(descriptor, labelParameter.RawValue);

            return labelParameter;
        }

        public static void FindAll(LabelDescriptor descriptor, string templateContent)
        {
            foreach (Match match in ParameterRegex.Matches(templateContent))
            {
                var parameter = Parse(descriptor, match);
                descriptor.Parameters.Add(parameter.Name.ToLower(), parameter);
            }
        }
    }

}

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

            var labelParameter = new LabelParameter { Name = name, Value = value };

            labelParameter.Function = LabelParameterFunctionParser.Parse(descriptor, labelParameter.Value);

            return labelParameter;
        }

        public static Dictionary<string, LabelParameter> FindAll(LabelDescriptor descriptor, string templateContent)
        {
            var result = new Dictionary<string, LabelParameter>();
            foreach (Match match in ParameterRegex.Matches(templateContent))
            {
                var parameter = Parse(descriptor, match);
                result.Add(parameter.Name.ToLower(), parameter);
            }
            return result;
        }
    }

}

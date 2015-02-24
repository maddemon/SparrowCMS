using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace SparrowCMS.Parsers
{
    public class LabelParameterDescriptorParser
    {
        private static readonly Regex ParameterRegex = new Regex(@"(?<name>\w+)\s*=\s*(?<value>""[^""]+""|[^\s\/]+|'[^']+')", RegexOptions.Compiled);

        private static LabelParameterDescriptor Parse(string labelName, Match match)
        {
            var name = match.Groups["name"].Value;
            var value = match.Groups["value"].Value.Trim('"').Trim('\'');

            var labelParameter = new LabelParameterDescriptor { Name = name, RawValue = value };

            labelParameter.Function = LabelParameterDescriptorFunctionParser.Parse(labelName, labelParameter.RawValue);

            return labelParameter;
        }

        public static Dictionary<string, LabelParameterDescriptor> FindAll(string labelName, string templateContent)
        {
            var result = new Dictionary<string, LabelParameterDescriptor>();
            foreach (Match match in ParameterRegex.Matches(templateContent))
            {
                var parameter = Parse(labelName, match);
                result.Add(parameter.Name.ToLower(), parameter);
            }
            return result;
        }
    }

}

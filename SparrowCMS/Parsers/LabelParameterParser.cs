using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace SparrowCMS.Parsers
{
    public class LabelParameterParser
    {
        private static readonly Regex ParameterRegex = new Regex(@"(?<name>\w+)\s*=\s*(?<value>""[^""]+""|[^\s\/]+|'[^']+')", RegexOptions.Compiled);

        private static LabelParameter Parse(string labelName, Match match)
        {
            var name = match.Groups["name"].Value;
            var value = match.Groups["value"].Value.Trim('"').Trim('\'');

            var labelParameter = new LabelParameter { Name = name, Value = value };

            labelParameter.Function = LabelParameterFunctionParser.Parse(labelName, value);

            return labelParameter;
        }

        public static Dictionary<string, LabelParameter> FindAll(string labelName, string templateContent)
        {
            var result = new Dictionary<string, LabelParameter>();
            foreach (Match match in ParameterRegex.Matches(templateContent))
            {
                var parameter = Parse(labelName, match);
                result.Add(parameter.Name.ToLower(), parameter);
            }
            return result;
        }
    }

    public class LabelParameterFunctionParser
    {
        private static readonly Regex ParameterFunctionRegex = new Regex(@"(?<name>[^)(]+)\([^)(]+\)", RegexOptions.Compiled);

        public static ILabelParameterFunction Parse(string labelName, string value)
        {
            var match = ParameterFunctionRegex.Match(value);
            if (match == null || match.Groups.Count <= 1) return null;
            return Factory.Instance.GetInstance<ILabelParameterFunction>(labelName, match.Groups["name"].Value);
        }

    }
}

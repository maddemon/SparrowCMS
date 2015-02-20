using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace SparrowCMS.Parsers
{
    public class LabelParameterParser
    {

        private static LabelParameter Parse(string labelName, Match match)
        {
            var name = match.Groups["name"].Value;
            var value = match.Groups["value"].Value.Trim('"').Trim('\'');

            var labelParameter = new LabelParameter { Name = name, Value = value };

            labelParameter.Function = ParseFunction(labelName, value);

            return labelParameter;
        }
        private static Regex functionRegex = new Regex(@"(?<name>[^)(]+)\([^)(]+\)", RegexOptions.Compiled);
        private static ILabelParameterFunction ParseFunction(string labelName,string value)
        {
            var match = functionRegex.Match(value);
            if (match == null || match.Groups.Count <= 1) return null;
            return Factory.Instance.GetInstance<ILabelParameterFunction>(labelName, match.Groups["name"].Value);
        }

        private static readonly Regex Regex = new Regex(@"(?<name>\w+)\s*=\s*(?<value>""[^""]+""|[^\s\/]+|'[^']+')", RegexOptions.Compiled);

        public static Dictionary<string, LabelParameter> Parse(string labelName, string parametersTemplateContent)
        {
            var result = new Dictionary<string, LabelParameter>();
            foreach (Match match in Regex.Matches(parametersTemplateContent))
            {
                var parameter = Parse(labelName, match);
                result.Add(parameter.Name.ToLower(), parameter);
            }
            return result;
        }
    }
}

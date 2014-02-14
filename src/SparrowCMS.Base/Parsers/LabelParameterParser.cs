using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace SparrowCMS.Core.Parsers
{
    public class LabelParameterParser
    {
        private static LabelParameter Parse(string labelName, Match match)
        {
            var name = match.Groups["name"].Value;
            var value = match.Groups["value"].Value.Trim('"').Trim('\'');

            var labelParameter = new LabelParameter {Name = name, Value = value};

            if (value.Contains('(') && value.Contains(')'))
            {
                labelParameter.ParameterFunction = Factory.Instance.GetInstance<IParameterFunction>(labelName, value);
            }
            return labelParameter;
        }

        private static readonly Regex Regex = new Regex(@"(?<name>\w+)\s*=\s*(?<value>""[^""]+""|[^\s\/]+|'[^']+')", RegexOptions.Compiled);

        public static Dictionary<string, LabelParameter> Parse(string labelName, string parametersTemplateContent)
        {
            var result = new Dictionary<string, LabelParameter>();
            foreach (Match match in Regex.Matches(parametersTemplateContent))
            {
                var parameter = Parse(labelName,match);
                result.Add(parameter.Name, parameter);
            }
            return result;
        }
    }
}

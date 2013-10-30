using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using SparrowCMS.Base.Managers;

namespace SparrowCMS.Base.Parsers
{
    public class LabelParameterParser
    {
        private static LabelParameter Parse(string labelName, Match match)
        {
            var name = match.Groups["name"].Value;
            var value = match.Groups["value"].Value.Trim('"').Trim('\'');

            var labelParameter = new LabelParameter();
            labelParameter.Name = name;
            labelParameter.Value = value;

            if (value.Contains('(') && value.Contains(')'))
            {
                labelParameter.ParameterFunction = Factory.Instance.GetInstance<IFunction>(labelName, value);
            }
            return labelParameter;
        }

        private static Regex _regex = new Regex(@"(?<name>\w+)\s*=\s*(?<value>""[^""]+""|[^\s\/]+|'[^']+')", RegexOptions.Compiled);

        public static IEnumerable<LabelParameter> Parse(string labelName, string parametersTemplateContent)
        {
            foreach (Match match in _regex.Matches(parametersTemplateContent))
            {
                yield return Parse(labelName, match);
            }
        }
    }
}

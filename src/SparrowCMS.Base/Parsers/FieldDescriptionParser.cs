using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using SparrowCMS.Base.Managers;

namespace SparrowCMS.Base.Parsers
{
    public class FieldDescriptionParser
    {
        //(?<!@)@((?<name>(\w+))|(\((?<name>\w+)(?<parameters>(\s\w+\s?=\s?("[^"]+"|[^\s]+))*)\)))
        private static Regex _regex = new Regex(@"(?<!@)@((?<name>(\w+))|(\((?<name>\w+)(?<parameters>(\s\w+\s?=\s?(""[^""]+""|[^\s]+))*)\)))", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        private static FieldDescription Parse(string labelName, Match match)
        {
            if (match == null)
            {
                return null;
            }

            var fieldName = match.Groups["name"].Value;

            var desc = new FieldDescription
            {
                FieldName = fieldName,
                TemplateContent = match.Groups[0].Value,
                Parameters = FieldParameterParser.Parse(labelName, fieldName, match.Groups["parameters"].Value),

            };

            return desc;
        }

        public static IEnumerable<FieldDescription> Parse(string labelName, string templateContent)
        {
            foreach (Match m in _regex.Matches(templateContent))
            {
                yield return Parse(labelName, m);
            }
        }
    }
}

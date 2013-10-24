using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using SparrowCMS.Base.Managers;

namespace SparrowCMS.Base.Parsers
{
    public class FieldParser
    {
        //(?<!@)@((?<name>(\w+))|(\((?<name>\w+)(?<parameters>(\s\w+\s?=\s?("[^"]+"|[^\s]+))*)\)))
        private static Regex _regex = new Regex(@"(?<!@)@((?<name>(\w+))|(\((?<name>\w+)(?<parameters>(\s\w+\s?=\s?(""[^""]+""|[^\s]+))*)\)))", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        private static Field Parse(string labelName, Match match)
        {
            if (match == null)
            {
                return null;
            }

            var fieldName = match.Groups["name"].Value;

            var field = FieldFactory.GetInstance(labelName, fieldName);
            field.Name = fieldName;
            field.Parameters = FieldParameterParser.Parse(labelName, fieldName, match.Groups["parameters"].Value);
            field.TemplateContent = match.Groups[0].Value;
            return field;
        }

        public static IEnumerable<Field> Parse(string labelName, string templateContent)
        {
            foreach (Match m in _regex.Matches(templateContent))
            {
                yield return Parse(labelName, m);
            }
        }
    }
}

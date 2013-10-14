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
        private static Regex _fieldPattern = new Regex(@"(?<!@)@((?<name>(\w+))|(\((?<name>\w+)(?<parameters>(\s\w+\s?=\s?(""[^""]+""|[^\s]+))*)\)))", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        private static Field Parse(string labelName, Match match)
        {
            if (match == null) return null;

            var fieldName = match.Groups["name"].Value;
            var field = FieldFactory.GetInstance(labelName, fieldName);
            if (field == null)
            {
                field = new Field
                {
                    Name = fieldName,
                    Parameters = FieldParameterParser.Parse(match.Groups["parameters"].Value),
                };
            }

            return field;
        }

        public static IEnumerable<Field> Parse(string labelName, string templateContent)
        {
            foreach (Match m in _fieldPattern.Matches(templateContent))
            {
                yield return Parse(labelName, m);
            }
        }
    }
}

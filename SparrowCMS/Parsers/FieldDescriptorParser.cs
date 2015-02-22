using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace SparrowCMS.Parsers
{
    public class FieldDescriptorParser
    {
        //(?<!@)@((?<name>(\w+))|(\((?<name>\w+)(?<parameters>(\s\w+\s?=\s?("[^"]+"|[^\s]+))*)\)))
        private static readonly Regex Regex = new Regex(@"(?<!@)@((?<name>([\w\.\-]+))|(\((?<name>[\w\.\-]+)(?<function>(\s\w+\s?=\s?(""[^""]+""|[^\s]+))*)\)))", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        private static FieldDescriptor Parse(string labelName, Match match)
        {
            if (match == null)
            {
                return null;
            }

            var fieldName = match.Groups["name"].Value;

            var desc = new FieldDescriptor
            {
                LabelName = labelName,
                FieldName = fieldName,
                TemplateContent = match.Groups[0].Value,
            };
            desc.Functions = FieldFunctionParser.FindAll(desc, match.Groups["function"].Value);
            return desc;
        }

        public static IEnumerable<FieldDescriptor> FindAll(string labelName, string templateContent)
        {
            foreach (Match m in Regex.Matches(templateContent))
            {
                yield return Parse(labelName, m);
            }
        }
    }
}

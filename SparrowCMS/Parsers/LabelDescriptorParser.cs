using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace SparrowCMS.Parsers
{
    public class LabelDescriptorParser
    {
        private static readonly Regex Regex = new Regex(@"{(?<name>[\w\.]+)(?<parameters>(\s+\w+\s*=\s*(""[^""]+""|[^\s\/]+|'[^']+'))*)\s*}(?<inner>[\s\S]*?){/\k<name>}|{(?<name>[\w.]+)(?<parameters>(\s+\w+\s*=\s*(""[^""]+""|[^\s\/]+|'[^']+'))*)\s*/}", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        private static LabelDescriptor Parse(Match match)
        {
            if (match == null)
            {
                return null;
            }

            var labelName = match.Groups["name"].Value;
            var className = "Default";
            var parameters = match.Groups["parameters"].Value;
            if (labelName.Contains("."))
            {
                className = labelName.Split('.')[1];
                labelName = labelName.Split('.')[0];
            }


            var desc = new LabelDescriptor
            {
                ID = Guid.NewGuid().ToString(),
                LabelName = labelName,
                ClassName = className,
                TemplateContent = match.Groups[0].Value,
                Parameters = LabelParameterParser.Parse(labelName, parameters),
                InnerHtml = match.Groups["inner"].Value
            };

            if (string.IsNullOrEmpty(desc.InnerHtml))
            {
                return desc;
            }

            desc.FieldDescriptors = FieldDescriptorParser.Parse(labelName + (className == "Default" ? null : "." + className), desc.InnerHtml);

            //desc.InnerLabelDescriptions = Parse(desc.InnerHtml);

            return desc;
        }

        public static IEnumerable<LabelDescriptor> Parse(string templateContent)
        {
            foreach (Match m in Regex.Matches(templateContent))
            {
                yield return Parse(m);
            }
        }

    }
}

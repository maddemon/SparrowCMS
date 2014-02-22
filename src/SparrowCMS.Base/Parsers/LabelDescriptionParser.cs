using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace SparrowCMS.Core.Parsers
{
    public class LabelDescriptionParser
    {
        private static readonly Regex Regex = new Regex(@"{(?<name>[\w\.]+)(?<parameters>(\s+\w+\s*=\s*(""[^""]+""|[^\s\/]+|'[^']+'))*)\s*}(?<inner>[\s\S]*?){/\k<name>}|{(?<name>[\w.]+)(?<parameters>(\s+\w+\s*=\s*(""[^""]+""|[^\s\/]+|'[^']+'))*)\s*/}", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        private static LabelDescription Parse(Match match)
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


            var desc = new LabelDescription
            {
                ID = Guid.NewGuid(),
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

            desc.FieldDescriptions = FieldDescriptionParser.Parse(labelName + (className == "Default" ? null : "." + className), desc.InnerHtml);

            //desc.InnerLabelDescriptions = Parse(desc.InnerHtml);

            return desc;
        }

        public static IEnumerable<LabelDescription> Parse(string templateContent)
        {
            foreach (Match m in Regex.Matches(templateContent))
            {
                yield return Parse(m);
            }
        }

    }
}

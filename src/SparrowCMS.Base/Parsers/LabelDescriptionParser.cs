using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace SparrowCMS.Base.Parsers
{
    public class LabelDescriptionParser
    {
        private static readonly Regex Regex = new Regex(@"{(?<name>[\w.]+)(?<parameters>(\s+\w+\s*=\s*(""[^""]+""|[^\s\/]+|'[^']+'))*)\s*}(?<inner>[\s\S]*?){/(?<name>[\w.]+)}|{(?<name>[\w.]+)(?<parameters>(\s+\w+\s*=\s*(""[^""]+""|[^\s\/]+|'[^']+'))*)\s*/}", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        private static LabelDescription Parse(Match match)
        {
            if (match == null)
            {
                return null;
            }

            var labelName = match.Groups["name"].Value;
            var parameters = match.Groups["parameters"].Value;

            var desc = new LabelDescription
            {
                LabelName = labelName,
                TemplateContent =  match.Groups[0].Value,
                Parameters = LabelParameterParser.Parse(labelName, parameters),
                InnerHtml = match.Groups["inner"].Value
            };

            if (string.IsNullOrEmpty(desc.InnerHtml))
            {
                return desc;
            }

            desc.FieldDescriptions = FieldDescriptionParser.Parse(labelName, desc.InnerHtml);

            desc.InnerLabelDescriptions = Parse(desc.InnerHtml);

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

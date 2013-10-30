using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using SparrowCMS.Base.Managers;

namespace SparrowCMS.Base.Parsers
{
    public class LabelDescriptionParser
    {
        private static Regex _regex = new Regex(@"{(?<name>[\w.]+)(?<parameters>(\s+\w+\s*=\s*(""[^""]+""|[^\s\/]+|'[^']+'))*)\s*}(?<inner>[\s\S]*?){/(?<name>[\w.]+)}|{(?<name>[\w.]+)(?<parameters>(\s+\w+\s*=\s*(""[^""]+""|[^\s\/]+|'[^']+'))*)\s*/}", RegexOptions.IgnoreCase | RegexOptions.Compiled);

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
            foreach (Match m in _regex.Matches(templateContent))
            {
                yield return Parse(m);
            }
        }

    }
}

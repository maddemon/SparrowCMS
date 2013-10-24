using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using SparrowCMS.Base.Managers;

namespace SparrowCMS.Base.Parsers
{
    public class LabelParser
    {
        private static Regex _regex = new Regex(@"{(?<name>[\w.]+)(?<parameters>(\s\w+\s?=\s?(""[^""]+""|[^\s\/]+))*)}(?<inner>[\s\S]*?){/(?<name>[\w.]+)}|{(?<name>[\w.]+)(?<parameters>(\s\w+\s?=\s?(""[^""]+""|[^\s\/]+))*)/}", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        private static LabelBase Parse(Match match)
        {
            if (match == null) return null;

            var labelName = match.Groups["name"].Value;
            var parameters = match.Groups["parameters"].Value;

            var label = LabelFactory.GetInstance(labelName);
            if (label == null)
            {
                return null;
            }
            label.Parameters = LabelParameterParser.Parse(labelName, parameters.Split(' '));

            var innerContent = match.Groups["inner"].Value;
            if (string.IsNullOrEmpty(innerContent))
            {
                return label;
            }

            label.Fields = FieldParser.Parse(labelName, innerContent);

            label.InnerLables = Parse(innerContent);

            return label;
        }

        public static IEnumerable<LabelBase> Parse(string templateContent)
        {
            foreach (Match m in _regex.Matches(templateContent))
            {
                yield return Parse(m);
            }
        }

    }
}

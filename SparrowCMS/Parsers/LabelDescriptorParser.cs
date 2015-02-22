using System;
using System.Linq;
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
            //label类的名称默认是Default,比如有些labelName并不包含.符号 ,例如{Test /},则实际类名为Test.Default
            var parameters = match.Groups["parameters"].Value;

            var innerHtml =  match.Groups["inner"].Value;

            return new LabelDescriptor
            {
                ID = Guid.NewGuid().ToString(),
                LabelName = labelName,
                TemplateContent = match.Groups[0].Value,
                InnerHtml = innerHtml,
                ParameterDescriptors = LabelParameterDescriptorParser.FindAll(labelName,parameters),
                FieldDescriptors = FieldDescriptorParser.FindAll(labelName, innerHtml).ToList(),
            };
        }

        /// <summary>
        /// 获取所有的LabelDescriptor
        /// </summary>
        /// <param name="templateContent"></param>
        /// <returns></returns>
        public static List<LabelDescriptor> FindAll(string templateContent)
        {
            var list = new List<LabelDescriptor>();
            foreach (Match m in Regex.Matches(templateContent))
            {
                list.Add(Parse(m));
            }

            return list;
        }

    }
}

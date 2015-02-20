using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace SparrowCMS.Parsers
{
    public class FieldFunctionParser
    {
        private static readonly Regex Regex = new Regex(@"(?<name>\w+)\s*=\s*(""(?<value>[^""]+)""|'(?<value>[^']+)'|(?<value>[^\s)(]+))", RegexOptions.Compiled);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="labelName"></param>
        /// <param name="attributesTemplateContent">format="some text $this" dateformat="yyyy/MM/dd"</param>
        /// <returns></returns>
        public static IEnumerable<FieldFunction> Parse(string labelName, string fieldName, string attributesTemplateContent)
        {
            if (!string.IsNullOrEmpty(attributesTemplateContent))
            {
                foreach (Match m in Regex.Matches(attributesTemplateContent))
                {
                    var func = Parse(labelName, m);
                    if (func != null)
                    {
                        yield return func;
                    }
                }
            }
        }

        private static FieldFunction Parse(string labelName, Match m)
        {
            var name = m.Groups["name"].Value;
            var value = m.Groups["value"].Value;
            var attribute = Factory.Instance.GetInstance<FieldFunction>(labelName, name);
            if (attribute != null)
            {
                attribute.Name = name;
                attribute.Value = value;
            }
            return attribute;
        }
    }
}

using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace SparrowCMS.Core.Parsers
{
    public class FieldAttributeParser
    {
        private static readonly Regex Regex = new Regex(@"(?<name>\w+)\s*=\s*(""(?<value>[^""]+)""|'(?<value>[^']+)'|(?<value>[^\s)(]+))", RegexOptions.Compiled);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="labelName"></param>
        /// <param name="attributesTemplateContent">format="some text $this" dateformat="yyyy/MM/dd"</param>
        /// <returns></returns>
        public static IEnumerable<FieldAttribute> Parse(string labelName, string fieldName, string attributesTemplateContent)
        {
            if (!string.IsNullOrEmpty(attributesTemplateContent))
            {
                foreach (Match m in Regex.Matches(attributesTemplateContent))
                {
                    var parameter =  Parse(labelName, m);
                    if (parameter != null)
                    {
                        yield return parameter;
                    }
                }
            }
        }

        private static FieldAttribute Parse(string labelName, Match m)
        {
            var name = m.Groups["name"].Value;
            var value = m.Groups["value"].Value;
            var attribute = Factory.Instance.GetInstance<FieldAttribute>(labelName, name);
            if (attribute != null)
            {
                attribute.Name = name;
                attribute.Value = value;
            }
            return attribute;
        }
    }
}

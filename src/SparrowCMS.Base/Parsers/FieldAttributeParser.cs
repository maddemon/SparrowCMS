using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using SparrowCMS.Base.Managers;

namespace SparrowCMS.Base.Parsers
{
    public class FieldAttributeParser
    {
        private static Regex _regex = new Regex(@"(?<name>\w+)\s*=\s*(""(?<value>[^""]+)""|(?<value>[^\s]+))", RegexOptions.Compiled);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parametersTemplateContent">format="some text $this" dateformat="yyyy/MM/dd"</param>
        /// <returns></returns>
        public static IEnumerable<FieldAttribute> Parse(string labelName, string fieldName, string parametersTemplateContent)
        {
            if (!string.IsNullOrEmpty(parametersTemplateContent))
            {
                foreach (Match m in _regex.Matches(parametersTemplateContent))
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
            var parameter = Factory.Instance.GetInstance<FieldAttribute>(labelName, name);
            if (parameter != null)
            {
                parameter.Name = name;
                parameter.Value = value;
            }
            return parameter;
        }
    }
}

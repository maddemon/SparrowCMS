using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using SparrowCMS.Base.Managers;

namespace SparrowCMS.Base.Parsers
{
    public class FieldParameterParser
    {
        private static Regex _regex = new Regex(@"(?<name>\w+)\s*=\s*(""(?<value>[^""]+)""|(?<value>[^\s]+))", RegexOptions.Compiled);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parametersTemplateContent">format="some text $this" dateformat="yyyy/MM/dd"</param>
        /// <returns></returns>
        public static IEnumerable<FieldParameter> Parse(string labelName, string fieldName, string parametersTemplateContent)
        {
            if (!string.IsNullOrEmpty(parametersTemplateContent))
            {
                foreach (Match m in _regex.Matches(parametersTemplateContent))
                {
                    yield return Parse(labelName, m);
                }
            }
        }

        private static FieldParameter Parse(string labelName, Match m)
        {
            var name = m.Groups["name"].Value;
            var value = m.Groups["value"].Value;
            var parameter = Factory.Instance.GetInstance<FieldParameter>(labelName, name);
            if (parameter != null)
            {
                parameter.Value = value;
                return parameter;
            }
            return null;
        }
    }
}

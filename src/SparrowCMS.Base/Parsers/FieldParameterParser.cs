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
            if (string.IsNullOrEmpty(parametersTemplateContent))
            {
                yield return null;
            }

            foreach (Match m in _regex.Matches(parametersTemplateContent))
            {
                var name = m.Groups["name"].Value;
                var value = m.Groups["value"].Value;
                var parameter = FieldParameterFactory.GetInstance(labelName, name);
                parameter.Name = name;
                parameter.Value = value;
                yield return parameter;
            }
        }
    }
}

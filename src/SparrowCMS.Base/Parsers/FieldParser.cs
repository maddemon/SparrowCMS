using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using SparrowCMS.Base.Managers;

namespace SparrowCMS.Base.Parsers
{
    public class FieldParser
    {
        //(?<!@)@((?<name>(\w+))|(\((?<name>\w+)(?<parameters>(\s\w+\s?=\s?("[^"]+"|[^\s]+))*)\)))
        private static Regex _fieldPattern = new Regex(@"(?<!@)@((?<name>(\w+))|(\((?<name>\w+)(?<parameters>(\s\w+\s?=\s?(""[^""]+""|[^\s]+))*)\)))", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        /// <summary>
        /// 根据模板创建一个Field实例，如果是特殊Field，则反射具体类
        /// </summary>
        /// <param name="templateContent">
        /// @(CreateTime format="yyyy-MM-dd")
        /// @id
        /// @(title maxlength=20)
        /// {Category.Model id=@id}@Name{/Category.Model}
        /// </param>
        /// <returns></returns>
        public static Field Parse(string labelName, string templateContent)
        {
            if (!templateContent.StartsWith("@")) return null;

            var match = _fieldPattern.Match(templateContent);
            if (match == null) return null;

            var fieldName = match.Groups["name"].Value;
            var field = CustomFieldManager.FindField(labelName, fieldName);
            if (field == null)
            {
                field = new Field
                {
                    Name = fieldName,
                    Parameters = FieldParameterParser.Parse(match.Groups["parameters"].Value),
                };
            }

            return field;
        }
    }
}

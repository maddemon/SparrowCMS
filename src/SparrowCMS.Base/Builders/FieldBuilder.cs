using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SparrowCMS.Base
{
    internal class FieldBuilder
    {
        private static IField GetSpecialField(string labelName, string fieldName)
        {
            var className = labelName + "." + fieldName;
            return Cache<IField>.GetOrSet(className, () =>
            {
                var type = AssemblyHelper.GetType("Fields", className);
                var field = Activator.CreateInstance(type);
                return field == null ? null : (IField)field;
            });
        }

        //@(?<name>(\w+))|(\((?<name>\w+)(?<parameters>(\s\w+\s?=\s?(("[^"]+")|([^\s]+)))*)\))
        private static Regex _fieldPattern = new Regex(@"@(?<name>(\w+))|(\((?<name>\w+)(?<parameters>(\s\w+\s?=\s?((""[^""]+"")|([^\s]+)))*)\))", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        /// <summary>
        /// 根据模板创建一个Field实例，如果是特殊Field，则反射具体类
        /// </summary>
        /// <param name="templateContent">
        /// @(CreateTime format="yyyy-MM-dd")
        /// @id
        /// @(title maxlength=20)
        /// </param>
        /// <returns></returns>
        public static IField Build(string labelName, string templateContent)
        {
            if (!templateContent.StartsWith("@")) return null;

            var match = _fieldPattern.Match(templateContent);
            if (match == null) return null;

            var fieldName = match.Groups["name"].Value;
            var field = GetSpecialField(labelName, fieldName);
            if (field == null)
            {
                field = new FieldBase
                {
                    Name = fieldName,
                    Parameters = FieldParameterBuidler.Build(match.Groups["parameters"].Value),
                };
            }

            return field;
        }
    }
}

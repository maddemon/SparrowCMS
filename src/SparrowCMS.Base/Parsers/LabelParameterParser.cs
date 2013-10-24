using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SparrowCMS.Base.Managers;

namespace SparrowCMS.Base.Parsers
{
    public class LabelParameterParser
    {
        private static LabelParameter Parse(string labelName, string parameter)
        {
            var name = parameter.Split('=')[0].Trim();
            var value = parameter.Split('=')[1].Trim().Trim('\'').Trim('\"');

            var labelParameter = new LabelParameter();
            labelParameter.Name = name;
            labelParameter.Value = value;

            if (value.Contains('(') && value.Contains(')'))
            {
                labelParameter.ParameterFunction = LabelParameterFunctionFactory.GetInstance(labelName, value);
            }
            return labelParameter;
        }

        public static IEnumerable<LabelParameter> Parse(string labelName, string[] parameters)
        {
            foreach (var parameter in parameters)
            {
                yield return Parse(labelName, parameter);
            }
        }
    }
}

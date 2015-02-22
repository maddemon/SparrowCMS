using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SparrowCMS.Common;

namespace SparrowCMS
{
    public class LabelParameter
    {
        public virtual string Name { get; set; }

        public virtual string Value { get; set; }

        public virtual IParameterFunction Function { get; set; }

        public virtual object ConvertParameterValue(Type type)
        {
            var value = Value;
            if (Function != null)
            {
                value = Function.GetParameterValue(Value);
            }

            if (string.IsNullOrEmpty(value))
            {
                return type.GetDefaultValue();
            }

            return Convert.ChangeType(value, type);
        }
    }
}

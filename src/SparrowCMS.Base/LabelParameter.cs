using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SparrowCMS.Core
{
    public class LabelParameter
    {
        public virtual string Name { get; set; }

        public virtual string Value { get; set; }

        public virtual IFunction Function { get; set; }

        public virtual object ConvertParameterValue(Type type)
        {
            var value = Value;
            if (string.IsNullOrEmpty(value))
            {
                if (type.IsValueType) return Activator.CreateInstance(type);
                return null;
            }
            if (Function != null)
            {
                value = Function.GetParameterValue(Value);
            }

            return Convert.ChangeType(value, type);
        }
    }
}

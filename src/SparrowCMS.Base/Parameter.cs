using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SparrowCMS.Core.Common;

namespace SparrowCMS.Core
{
    public class Parameter
    {
        public virtual string Name { get; set; }

        public virtual string Value { get; set; }

        public virtual IFunction Function { get; set; }

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

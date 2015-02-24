using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SparrowCMS.Common;

namespace SparrowCMS
{
    public class LabelParameterDescriptor
    {
        public virtual string Name { get; set; }

        public virtual string RawValue { get; set; }

        public virtual ILabelParameterDescriptorFunction Function { get; set; }

        public virtual object GetFinalValue(Type type)
        {
            var result = RawValue;
            if (Function != null)
            {
                result = Function.GetValue(RawValue);
            }

            if (string.IsNullOrEmpty(result))
            {
                return type.GetDefaultValue();
            }

            return Convert.ChangeType(result, type);
        }
    }
}

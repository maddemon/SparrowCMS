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

        public virtual string ConvertParameterValue()
        {
            if (Function != null)
            {
                return Function.GetParameterValue(Value);
            }

            return Value;
        }
    }
}

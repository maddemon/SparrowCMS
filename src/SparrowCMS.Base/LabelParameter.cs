using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SparrowCMS.Base
{
    public class LabelParameter
    {
        public virtual string Name { get; set; }

        public virtual string Value { get; set; }

        public virtual IFunction ParameterFunction { get; set; }

        public virtual string GetReturnValue()
        {
            if (ParameterFunction != null)
            {
                return ParameterFunction.GetValue(Value);
            }

            return Value;
        }
    }
}

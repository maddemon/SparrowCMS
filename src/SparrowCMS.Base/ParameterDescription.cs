using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SparrowCMS.Base
{
    public class ParameterDescription
    {
        public string Name { get; set; }

        public string Value { get; set; }

        public IFunction Function { get; set; }

        public string GetRealValue()
        {
            if (Function != null)
            {
                return Function.GetValue(Value);
            }

            return Value;
        }
    }
}

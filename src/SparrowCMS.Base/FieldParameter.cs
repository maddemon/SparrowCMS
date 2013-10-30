using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SparrowCMS.Base
{
    public class FieldParameter
    {
        public virtual string Value { get; set; }

        public virtual string GetReturnValue(object fieldValue)
        {
            return fieldValue == null ? null : fieldValue.ToString();
        }
    }
}

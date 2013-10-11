using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SparrowCMS.Base
{
    public class FieldParameter
    {
        public virtual string Name { get; set; }

        public virtual string Value { get; set; }
        
        public object FieldValue { get; set; }

        public virtual string GetReturnValue()
        {
            return FieldValue.ToString();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SparrowCMS
{
    public class FieldFunction
    {
        public string Name { get; set; }

        public string RawValue { get; set; }

        public virtual string GetValue(object fieldValue)
        {
            return fieldValue == null ? null : fieldValue.ToString();
        }
    }
}

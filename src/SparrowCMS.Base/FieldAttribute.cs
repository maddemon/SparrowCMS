using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SparrowCMS.Base
{
    public class FieldAttribute
    {
        public string Name { get; set; }

        public string Value { get; set; }

        public virtual string ConvertFieldValue(object fieldValue)
        {
            return fieldValue == null ? null : fieldValue.ToString();
        }
    }
}

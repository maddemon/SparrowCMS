using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SparrowCMS.Base.Labels.Shared.Fields
{
    public class Format : FieldAttribute
    {
        public override string ConvertFieldValue(object fieldValue)
        {
            if (fieldValue == null)
                return null;

            return Value.Replace("$this", fieldValue.ToString());
        }
    }
}

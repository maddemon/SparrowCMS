using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SparrowCMS.Base.Labels.Shared.Fields
{
    public class Format : FieldParameter
    {
        public override string GetReturnValue(object fieldValue)
        {
            if (fieldValue == null)
                return null;

            return Value.Replace("$this", fieldValue.ToString());
        }
    }
}

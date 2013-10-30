using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SparrowCMS.Base.Labels.Shared.Fields
{
    public class DateFormat : FieldParameter
    {
        public override string GetReturnValue(object fieldValue)
        {
            if (fieldValue is DateTime)
            {
                var dateFormat = Value;
                return ((DateTime)fieldValue).ToString(dateFormat);
            }
            return base.GetReturnValue(fieldValue);
        }

    }
}

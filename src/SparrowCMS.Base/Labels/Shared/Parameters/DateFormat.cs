using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SparrowCMS.Base.Labels.Shared.Fields
{
    public class DateFormat : FieldAttribute
    {
        public override string ConvertFieldValue(object fieldValue)
        {
            if (fieldValue is DateTime)
            {
                var dateFormat = Value;
                return ((DateTime)fieldValue).ToString(dateFormat);
            }
            return base.ConvertFieldValue(fieldValue);
        }

    }
}

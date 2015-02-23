using System;

namespace SparrowCMS.Labels.Shared.Functions
{
    public class DateFormat : FieldFunction
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

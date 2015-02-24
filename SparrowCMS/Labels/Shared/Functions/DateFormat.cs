using System;

namespace SparrowCMS.Labels.Shared.Functions
{
    public class DateFormat : FieldFunction
    {
        public override string GetValue(object fieldValue)
        {
            if (fieldValue is DateTime)
            {
                var dateFormat = RawValue;
                return ((DateTime)fieldValue).ToString(dateFormat);
            }
            return base.GetValue(fieldValue);
        }

    }
}

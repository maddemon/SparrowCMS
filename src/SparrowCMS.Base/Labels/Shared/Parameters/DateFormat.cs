using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SparrowCMS.Base.Labels.Shared.Fields
{
    public class DateFormat : FieldParameter
    {
        public override string GetReturnValue()
        {
            return ((DateTime)FieldValue).ToString(Value);
        }
    }
}

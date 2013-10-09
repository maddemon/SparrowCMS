using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SparrowCMS.Base
{
    public class FieldBase : IField
    {
        public string Name { get; set; }

        public List<FieldParameter> Attributes { get; set; }

        public string TemplateContent { get; set; }

        public virtual string GetValue(object rowEntity)
        {
            var value = rowEntity.GetPropertyValueToString(Name);

            foreach (var attr in Attributes)
            {
                value = attr.GetFieldValue(value);
            }

            return value;
        }
    }
}

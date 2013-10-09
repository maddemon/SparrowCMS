using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SparrowCMS.Base
{
    public class FieldBase : IField
    {
        public string Name { get; set; }

        public IEnumerable<FieldParameter> Parameters { get; set; }

        public string TemplateContent { get; set; }

        public virtual string GetValue(object rowEntity)
        {
            var value = rowEntity.GetPropertyValueToString(Name);

            foreach (var attr in Parameters)
            {
                value = attr.GetFieldValue(value);
            }

            return value;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SparrowCMS.Base
{
    public class Field
    {
        public string Name { get; set; }

        public IEnumerable<FieldParameter> Parameters { get; set; }

        public string TemplateContent { get; set; }

        public virtual string GetValue(Document doc)
        {
            var fieldValue = doc[Name];

            var result = string.Empty;

            foreach (var p in Parameters)
            {
                result = p.GetReturnValue(fieldValue);
            }

            return string.IsNullOrEmpty(result) ? fieldValue.ToString() : result;
        }
    }
}

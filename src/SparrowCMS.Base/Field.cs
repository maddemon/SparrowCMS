using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SparrowCMS.Base
{
    public class Field
    {
        public string Name { get; set; }

        public IEnumerable<FieldAttribute> Attributes { get; set; }

        public string TemplateContent { get; set; }

        public virtual object GetValue(Document doc)
        {
            return doc[Name]; 
        }

        public virtual string GetReplacedContent(Document doc)
        {
            var fieldValue = GetValue(doc);
            var result = string.Empty;

            foreach (var p in Attributes)
            {
                result = p.ConvertFieldValue(fieldValue);
            }

            return string.IsNullOrEmpty(result) ? fieldValue.ToString() : result;
        }
    }
}

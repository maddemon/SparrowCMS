using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SparrowCMS.Core.Common;

namespace SparrowCMS.Core
{
    public class Field
    {
        public string Name { get; set; }

        public IEnumerable<FieldAttribute> Attributes { get; set; }

        public string TemplateContent { get; set; }

        public virtual object GetValue(Document doc)
        {
            if (doc == null) return null;
            if (Name.Contains("."))
            {
                Document subDoc = null;
                var names = Name.Split('.');
                for (var i = 0; i < names.Length - 1; i++)
                {
                    if (subDoc == null)
                    {
                        subDoc = doc[names[i]] as Document;
                    }
                    else
                    {
                        subDoc = subDoc[names[i]] as Document;
                    }
                }
                if (subDoc == null) { return null; }
                return subDoc[names[names.Length - 1]];
            }
            return doc[Name];
        }

        public virtual string GetReplacedContent(Document doc)
        {
            var fieldValue = GetValue(doc);
            if (fieldValue == null) return null;

            var result = string.Empty;

            if (Attributes != null)
            {
                foreach (var p in Attributes)
                {
                    result = p.ConvertFieldValue(fieldValue);
                }
            }
            return string.IsNullOrEmpty(result) ? fieldValue.ToString() : result;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SparrowCMS.Common;

namespace SparrowCMS
{
    public class DefaultField : IField
    {
        public string LabelName { get; set; }

        public string Name { get; set; }

        public IEnumerable<FieldFunction> Functions { get; set; }

        public string TemplateContent { get; set; }

        public virtual object GetValue(Document doc)
        {
            if (doc == null) return null;
            var prefix = LabelName + ".";
            if (Name.StartsWith(prefix))
            {
                Name = Name.Replace(prefix, "");
            }
            if (Name.Contains("."))
            {
                var names = Name.Split('.');
                Document subDoc = null;
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
                if (subDoc == null) { return TemplateContent; }
                return subDoc[names[names.Length - 1]];
            }
            return doc[Name];
        }

        public virtual string GetReplacedContent(Document doc)
        {
            var fieldValue = GetValue(doc);
            if (fieldValue == null) return null;

            var result = string.Empty;

            if (Functions != null)
            {
                foreach (var p in Functions)
                {
                    result = p.GetValue(fieldValue);
                }
            }
            return string.IsNullOrEmpty(result) ? fieldValue.ToString() : result;
        }
    }
}

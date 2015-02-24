using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SparrowCMS.Attributes;

namespace SparrowCMS.Common
{
    public static class DocumentExtensions
    {
        public static Document ToDocument<T>(this T data)
        {
            var doc = new Document();
            if (data == null) return doc;

            foreach (var p in data.GetType().GetProperties())
            {
                var attr = p.GetCustomAttributes(true).FirstOrDefault(e => e.GetType() == typeof(DocumentIgnoreAttribute));
                if (attr != null)
                {
                    continue;
                }
                var value = p.GetValue(data, null);
                if (value != null && !value.GetType().IsBasicType())
                {
                    doc[p.Name] = value.ToDocument();
                }
                else
                {
                    doc[p.Name] = value;
                }
            }
            return doc;
        }

    }


}

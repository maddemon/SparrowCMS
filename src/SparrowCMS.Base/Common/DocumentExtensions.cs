using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SparrowCMS.Core.Common
{
    public static class DocumentExtensions
    {
        public static Document ToDocument<T>(this T data)
        {
            var doc = new Document();
            foreach (var p in data.GetType().GetProperties())
            {
                var value =  p.GetValue(data, null);
                if (value != null && value.GetType().Module.ScopeName != "CommonLanguageRuntimeLibrary")
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

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

    public static class TypeExtensions
    {
        public static object GetDefaultValue(this Type t)
        {
            if (t.IsValueType && Nullable.GetUnderlyingType(t) == null)
                return Activator.CreateInstance(t);
            else
                return null;
        }

        public static object ToValue(this string value, Type type)
        {
            if (string.IsNullOrEmpty(value))
            {
                return type.GetDefaultValue();
            }

            if (type == typeof(Guid))
            {
                return Guid.Parse(value);
            }

            if (type.IsEnum)
            {
                return Enum.Parse(type, value, true);
            }

            if (type.IsBasicType())
            {
                return Convert.ChangeType(value, type);
            }


            return type.GetDefaultValue();
        }

        public static bool IsBasicType(this Type type)
        {
            return type.IsPrimitive || type == typeof(string) || type.IsEnum || type == typeof(Guid);
        }
    }

}

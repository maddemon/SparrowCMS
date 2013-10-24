using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SparrowCMS.Base
{
    public static class Extensions
    {
        public static T GetPropertyValue<T>(this object obj, string propertyName)
        {
            throw new NotImplementedException();
        }

        public static string GetPropertyValueToString(this object obj, string propertyName)
        {
            throw new NotImplementedException();
        }

        public static void AddValue<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key, TValue value)
        {
            if (!dict.ContainsKey(key))
            {
                dict.Add(key, value);
            }
        }

        public static TValue GetValue<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key)
        {
            return dict.ContainsKey(key) ? dict[key] : default(TValue);
        }
    }
}

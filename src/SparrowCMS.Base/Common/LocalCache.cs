using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SparrowCMS.Core.Common
{
    public class LocalCache
    {
        private static ConcurrentDictionary<string, object> _data = new ConcurrentDictionary<string, object>();

        public static T GetOrSet<T>(string key, Func<T> getValueFunc)
        {
            if (!_data.ContainsKey(key))
            {
                _data.TryAdd(key, getValueFunc());
            }
            return (T)_data[key];
        }

        public static T Get<T>(string key)
        {
            if (_data.ContainsKey(key))
            {
                return (T)_data[key];
            }

            return default(T);
        }

        public static void Set<T>(string key, T value)
        {
            _data.TryAdd(key, value);
        }

        public static void Remove(string key)
        { 
            object data;
            _data.TryRemove(key, out data);
        }
    }
}

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SparrowCMS.Core.Common
{
    public static class Cache<T>
    {
        private static ConcurrentDictionary<string, T> _data = new ConcurrentDictionary<string, T>();

        public static T GetOrSet(string key, Func<T> getValueFunc)
        {
            if (!_data.ContainsKey(key))
            {
                _data.TryAdd(key, getValueFunc());
            }
            return _data[key];
        }

        public static T Get(string key)
        {
            if (_data.ContainsKey(key))
            {
                return _data[key];
            }

            return default(T);
        }

        public static void Set(string key, T value)
        {
            _data.TryAdd(key, value);
        }
    }
}

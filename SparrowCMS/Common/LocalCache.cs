using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SparrowCMS.Common
{
    public class Cache
    {
        private static ConcurrentDictionary<string, object> _data = new ConcurrentDictionary<string, object>();

        public static T GetOrSet<T>(string key, Func<T> getValueFunc)
        {
#if DEBUG
            return getValueFunc();
#endif
            if (!_data.ContainsKey(key))
            {
                _data.TryAdd(key, getValueFunc());
            }
            return (T)_data[key];
        }

        public static T Get<T>(string key)
        {
#if DEBUG
            return default(T);
#endif

            if (_data.ContainsKey(key))
            {
                return (T)_data[key];
            }

            return default(T);
        }

        public static void Set<T>(string key, T value)
        {
#if DEBUG
            return;
#endif
            _data.TryAdd(key, value);
        }

        public static void Remove(string key)
        {
#if DEBUG
            return;
#endif

            object data;
            _data.TryRemove(key, out data);
        }
    }
}

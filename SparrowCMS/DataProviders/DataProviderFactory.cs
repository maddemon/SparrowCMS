using SparrowCMS.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.IO;

namespace SparrowCMS.DataProviders
{
    public class DataProviderFactory
    {
        private static Assembly GetAssembly()
        {
            var dbType = AppSettings.Current["DbType"];
            if (string.IsNullOrEmpty(dbType))
            {
                throw new Exception("web.config miss dbType section");
            }
            var filePath = Path.Combine(Environment.CurrentDirectory, "SparrowCMS.DataProvider." + dbType + ".dll");
            return Assembly.LoadFile(filePath);
        }

        public static T GetProvider<T>()
        {
            var dbType = AppSettings.Current["DbType"];
            if (string.IsNullOrEmpty(dbType))
            {
                throw new Exception("web.config miss dbType section");
            }
            var typeName = typeof(T).FullName;
            return Cache.GetOrSet<T>(typeName, () =>
            {
                var ass = GetAssembly();
                var type = ass.GetTypes().FirstOrDefault(e => e.GetInterface(typeof(T).Name, true) != null);
                return (T)Activator.CreateInstance(type);
            });
        }
    }
}

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

            var fullName = "SparrowCMS.DataProvider." + dbType + ".dll";
            var filePath = Path.Combine(Environment.CurrentDirectory, fullName);
            if (!File.Exists(filePath))
            {
                filePath = Path.Combine(Environment.CurrentDirectory, "bin", fullName);
            }
            return Assembly.LoadFrom(filePath);
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
                var type = ass.GetTypes().FirstOrDefault(t => typeof(T).IsAssignableFrom(t));
                return (T)Activator.CreateInstance(type);
            });
        }
    }
}

using SparrowCMS.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

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

            return Assembly.Load("SparrowCMS.DataProvider." + dbType + ".dll");
        }

        public static T GetProvider<T>()
        {
            var typeName = typeof(T).Name;
            return Cache.GetOrSet<T>(typeName, () =>
            {
                var ass = GetAssembly();
                return (T)ass.CreateInstance(typeName, true);
            });
        }
    }
}

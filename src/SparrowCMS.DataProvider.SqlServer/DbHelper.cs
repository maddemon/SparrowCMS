using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SparrowCMS.DAL.SqlServer
{
    public class DbHelper
    {
        private static readonly string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings[0].ConnectionString;

        public static DataContext GetDbContext()
        {
            return new DataContext(connectionString);
        }
    }
}

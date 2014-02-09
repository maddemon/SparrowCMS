using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SparrowCMS.DAL.SqlServer
{
    public class DbHelper
    {
        public static DBDataContext GetDataContext()
        {
            return new DBDataContext();
        }
    }
}

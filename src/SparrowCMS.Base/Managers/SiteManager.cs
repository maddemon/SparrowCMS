using SparrowCMS.Base.IDataProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SparrowCMS.Base.Managers
{
    public class SiteManager
    {
        private ISiteDataProvider _dao;
        public static Site GetCurrentSite(string host)
        {
            return new Site();
        }
    }
}

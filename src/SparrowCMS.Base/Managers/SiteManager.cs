using SparrowCMS.Core.DataProviders;
using SparrowCMS.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SparrowCMS.Core.Managers
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

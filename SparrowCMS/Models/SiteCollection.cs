using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SparrowCMS.DataProviders;
using SparrowCMS.Models;

namespace SparrowCMS
{
    public class SiteCollection
    {
        private ISiteDataProvider _db;

        public SiteCollection()
        {
            _db = DataProviderFactory.GetProvider<ISiteDataProvider>();
        }

        public Site GetSite(Uri uri)
        {
            var sites = _db.GetSites();
            return sites.FirstOrDefault(e => e.Domains != null && e.Domains.Contains(uri.Host) || e.Domains.Contains(uri.Host + ":" + uri.Port));
        }
    }
}

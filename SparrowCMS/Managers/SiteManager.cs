using SparrowCMS.DataProviders;
using SparrowCMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SparrowCMS.Common;

namespace SparrowCMS.Managers
{
    public class SiteManager
    {
        private ISiteDataProvider _dataProvider;

        private string _cacheKey = "cms_sites";

        public SiteManager()
        {
            _dataProvider = DataProviderFactory.GetProvider<ISiteDataProvider>();
        }

        public List<Site> GetSites()
        {
            return Cache.GetOrSet<List<Site>>(_cacheKey, () => _dataProvider.GetSites());
        }

        public Site GetSite(string idOrHost)
        {
            //TODO
            return new Site();
            return GetSites().FirstOrDefault(e => e.Domains.Contains(idOrHost) || e.Id == idOrHost);
        }

        public void AddSite(Site site)
        {

        }
    }
}

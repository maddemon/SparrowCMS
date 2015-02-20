using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SparrowCMS.DataProviders;
using SparrowCMS.Models;

namespace SparrowCMS
{
    public class PageCollection
    {
        private IPageDataProvider _db;
        public PageCollection()
        {
            _db = DataProviderFactory.GetProvider<IPageDataProvider>();
        }

        public Page GetPage(Site site,Uri requestUri)
        {
            var pages = _db.GetSitePages(site);
            return pages.FirstOrDefault(e => e.UrlRoute.IsMatch(requestUri.PathAndQuery));
        }

        public Page GetPage(string id)
        {
            return _db.GetPage(id);
        }
    }
}

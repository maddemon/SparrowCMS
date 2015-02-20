using SparrowCMS.Common;
using SparrowCMS.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml.Linq;
using SparrowCMS.DataProviders;

namespace SparrowCMS.Managers
{
    public class PageManager
    {
        private IPageDataProvider _dataProvider;
        public PageManager()
        {
            _dataProvider = DataProviderFactory.GetProvider<IPageDataProvider>();
        }

        private static string _cacheKey = "cms_pages";

        public List<Page> GetPages(Site site)
        {
            var pages = Cache.GetOrSet<List<Page>>(_cacheKey, () => _dataProvider.GetPages());
            return pages.Where(e => e.SiteId == site.Id).ToList();
        }

        private string GetConfigPath()
        {
            return Path.Combine(Environment.CurrentDirectory, "configs/pages.config");
        }

        public Page GetPage(Site site, string url)
        {
            var pages = GetPages(site);
            
            var currentPage = GetPages(site).FirstOrDefault(page => page.UrlRoute.IsMatch(url));

            if (currentPage == null)
            {
                throw new HttpException(404, "PAGE NOT FOUND!");
            }

            return currentPage;
        }

        public void Save(Page model)
        {
            _dataProvider.SavePage(model);
            Cache.Remove(_cacheKey);
        }
    }
}

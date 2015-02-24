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
            var pages = _dataProvider.GetPages();
            return site == null ? pages : pages.Where(e => e.SiteId == site.Id).ToList();
        }

        private string GetConfigPath()
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "configs/pages.config");
        }

        public Page GetPage(string pageId)
        {
            return GetPages(null).FirstOrDefault(p => p.Id == pageId);
        }

        public Page GetPage(Site site, string url)
        {
            var currentPage = GetPages(site).FirstOrDefault(page => page.UrlRoute.IsMatch(url));

            if (currentPage == null)
            {
                throw new Exceptions.PageNotFoundException();
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SparrowCMS.Base.Managers
{
    public class PageManager
    {
        private static string _cacheKye = "cms_pages";
        
        private static IEnumerable<Page> GetPages(Site site)
        {
            return Cache<List<Page>>.GetOrSet(_cacheKye, GetPagesFromDb);
        }

        private static List<Page> GetPagesFromDb()
        {
            return new List<Page>();
        }

        public static Page GetCurrentPage(Site site, System.Web.HttpContext context)
        {
            Page currentPage = null;

            foreach (var page in GetPages(site))
            {
                if (page.UrlRoute.IsMatch(context.Request.Url.AbsolutePath))
                {
                    currentPage = page;
                    break;
                }
            }

            currentPage.Init();

            return currentPage;
        }
    }
}

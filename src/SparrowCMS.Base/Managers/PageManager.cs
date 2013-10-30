using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SparrowCMS.Base.Managers
{
    public class PageManager
    {
        public static IEnumerable<Page> GetPages(Site site)
        {
            throw new NotImplementedException();
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

            if (!currentPage.Initialized)
            {
                currentPage.Init();
            }

            return currentPage;
        }
    }
}

using SparrowCMS.Core.Common;
using SparrowCMS.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml.Linq;

namespace SparrowCMS.Core.Managers
{
    public class PageManager
    {
        private static string _cacheKye = "cms_pages";

        private static IEnumerable<Page> GetPages(Site site)
        {
            return ShareCache.GetOrSet<List<Page>>(_cacheKye, GetPagesFromConfig);
        }

        private static List<Page> GetPagesFromConfig()
        {
            var filePath = Path.Combine(Environment.CurrentDirectory, "configs/pages.config");
            var doc = XDocument.Load(filePath);
            var result = new List<Page>();

            foreach (var pageElement in doc.Root.Elements("page"))
            {
                var page = new Page
                {
                    Name = pageElement.Element("name").Value,
                    UrlPattern = pageElement.Element("url").Value,
                };

                page.UrlRoute = new UrlRoute(page.UrlPattern);

                if (pageElement.Element("role") != null)
                {
                    page.Role = (Role)Enum.Parse(typeof(Role), pageElement.Element("role").Value, true);
                }

                var templateElement = pageElement.Element("template");
                var layout = templateElement.Attribute("layout") == null ? null : templateElement.Attribute("layout").Value;

                page.Template = new Template(templateElement.Value, layout);

                result.Add(page);
            }

            return result.OrderByDescending(e => e.UrlPattern).ToList();
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

            if (currentPage == null)
            {
                throw new HttpException(404, "PAGE NOT FOUND!");
            }

            currentPage.Init();

            return currentPage;
        }
    }
}

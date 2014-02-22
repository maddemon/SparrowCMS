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
        private static string _cacheKey = "cms_pages";

        public static List<Page> GetPages(Site site)
        {
            return ShareCache.GetOrSet<List<Page>>(_cacheKey, GetPagesFromConfig);
        }

        private static string GetConfigPath()
        {
            return Path.Combine(Environment.CurrentDirectory, "configs/pages.config");
        }

        private static List<Page> GetPagesFromConfig()
        {
            var doc = XDocument.Load(GetConfigPath());
            var result = new List<Page>();

            foreach (var pageElement in doc.Root.Elements("page"))
            {
                var page = new Page
                {
                    Name = pageElement.Element("name").Value,
                    UrlPattern = pageElement.Element("url").Value,
                };

                var idAttr = pageElement.Attribute("id");
                if (idAttr == null)
                {
                    page.Id = Guid.NewGuid();
                }
                else
                {
                    page.Id = Guid.Parse(idAttr.Value);
                }

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

            SaveConfigFile(result);

            return result.OrderByDescending(e => e.UrlPattern).ToList();
        }

        public static Page GetCurrentPage(Site site, Context context)
        {
            var currentPage = GetPages(site).FirstOrDefault(page => page.UrlRoute.IsMatch(context.HttpContext.Request.Url.AbsolutePath));

            if (currentPage == null)
            {
                throw new HttpException(404, "PAGE NOT FOUND!");
            }

            currentPage.Init(context);

            return currentPage;
        }

        public static void Save(Models.Page model)
        {
            
            var pages = GetPages(null);
            var index = pages.FindIndex(e => e.Id == model.Id);
            if (index > -1)
            {
                pages[index] = model;
            }
            else
            {
                pages.Add(model);
            }

            ShareCache.Set(_cacheKey, pages);
            SaveConfigFile(pages);
        }

        private static void SaveConfigFile(IEnumerable<Page> pages)
        {
            var doc = new XDocument();
            var root = new XElement("pages");
            foreach (var page in pages)
            {
                var node = new XElement("page");
                node.SetAttributeValue("id", page.Id.ToString());
                node.Add(new XElement("name", page.Name));
                node.Add(new XElement("role", page.Role));
                node.Add(new XElement("url", page.UrlPattern));

                var templateNode= new XElement("template",page.Template.FilePath);
                if(!string.IsNullOrEmpty(page.Template.Layout))
                {
                    templateNode.SetAttributeValue("layout",page.Template.Layout);
                }
                node.Add(templateNode);
                root.Add(node);
            }
            doc.Add(root);

            File.WriteAllText(GetConfigPath(), doc.ToString());
        }
    }
}

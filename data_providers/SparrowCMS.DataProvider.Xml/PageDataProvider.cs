using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml.Linq;
using SparrowCMS.Common;
using SparrowCMS.DataProviders;
using SparrowCMS.Models;

namespace SparrowCMS.DataProviders.Xml
{
    internal class PageDataProvider : IPageDataProvider
    {
        private string GetConfigPath()
        {
            return Path.Combine(Environment.CurrentDirectory, "configs/pages.config");
        }

        private List<Page> GetPagesFromConfig()
        {
            var doc = XDocument.Load(GetConfigPath());
            var result = new List<Page>();

            foreach (var pageElement in doc.Root.Elements("page"))
            {
                var page = new Page
                {
                    Title = pageElement.Element("name").Value,
                    UrlPattern = pageElement.Element("url").Value,
                };

                var idAttr = pageElement.Attribute("id");
                if (idAttr == null)
                {
                    page.Id = Guid.NewGuid().ToString();
                }
                else
                {
                    page.Id = idAttr.Value;
                }

                page.UrlRoute = new UrlRoute { Url = page.UrlPattern };

                if (pageElement.Element("role") != null)
                {
                    page.Role = (Role)Enum.Parse(typeof(Role), pageElement.Element("role").Value, true);
                }

                var templateElement = pageElement.Element("template");
                var layout = templateElement.Attribute("layout") == null ? null : templateElement.Attribute("layout").Value;
                page.Template = GetTemplate(templateElement.Value, layout);
                result.Add(page);
            }

            SaveConfigFile(result);

            return result.OrderByDescending(e => e.UrlPattern).ToList();
        }

        private Template GetTemplate(string templatePath, string layoutPath)
        {
            var filePath = Path.Combine(Environment.CurrentDirectory, templatePath);
            var templateContent = File.ReadAllText(templatePath);

            if (!string.IsNullOrEmpty(layoutPath))
            {
                var layoutContent = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, layoutPath));
                templateContent = layoutContent.Replace("%body%", templateContent);
            }

            return new Template
            {
                Content = templateContent,
            };
        }

        private void SaveConfigFile(IEnumerable<Page> pages)
        {
            var doc = new XDocument();
            var root = new XElement("pages");
            foreach (var page in pages)
            {
                var node = new XElement("page");
                node.SetAttributeValue("id", page.Id.ToString());
                node.Add(new XElement("name", page.Title));
                node.Add(new XElement("role", page.Role));
                node.Add(new XElement("url", page.UrlPattern));

                var templateNode = new XElement("template", page.Template);

                node.Add(templateNode);
                root.Add(node);
            }
            doc.Add(root);

            File.WriteAllText(GetConfigPath(), doc.ToString());
        }

        public List<Page> GetSitePages(Site site)
        {
            return GetPages().Where(e => e.SiteId == site.Id).ToList();
        }

        public List<Page> GetPages()
        {
            return GetPagesFromConfig();
        }

        public Page GetPage(string id)
        {
            return GetPages().FirstOrDefault(e => e.Id == id);
        }


        public void SavePage(Page model)
        {
            throw new NotImplementedException();
        }
    }
}

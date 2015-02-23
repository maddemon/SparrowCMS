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
                page.Template = GetTemplate(templateElement);
                result.Add(page);
            }

            //SaveConfigFile(result);

            return result.OrderByDescending(e => e.UrlPattern).ToList();
        }

        private Template GetTemplate(XElement templateElement)
        {
            var layoutPath = templateElement.Attribute("layout") == null ? null : templateElement.Attribute("layout").Value;
            var templatePath = templateElement.Value;
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, templatePath);
            var templateContent = string.Empty;
            System.Diagnostics.Trace.WriteLine("FilePath:" + filePath);
            if (File.Exists(filePath))
            {
                templateContent = File.ReadAllText(templatePath);

                if (!string.IsNullOrEmpty(layoutPath))
                {
                    var layoutContent = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, layoutPath));
                    templateContent = layoutContent.Replace("%body%", templateContent);
                }
            }

            return new Template
            {
                FilePath = templatePath,
                Content = templateContent,
                Layout =  layoutPath
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

                var templateNode = new XElement("template", page.Template.FilePath);

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

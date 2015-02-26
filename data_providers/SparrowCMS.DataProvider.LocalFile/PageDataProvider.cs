using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml.Linq;
using SparrowCMS.Common;
using SparrowCMS.DataProvider.Xml;
using SparrowCMS.DataProviders;
using SparrowCMS.Models;

namespace SparrowCMS.DataProviders.LocalFile
{
    internal class PageDataProvider : IPageDataProvider
    {
        private static readonly SiteDataProvider SiteDataProvider = new SiteDataProvider();

        public List<Page> GetPages(Site site = null)
        {
            if (site == null)
            {
                var sites = SiteDataProvider.GetSites();
                return sites.SelectMany(s => s.Pages).ToList();
            }

            return site.Pages;
        }

        public Page GetPage(string id)
        {
            return GetPages().FirstOrDefault(e => e.Id == id);
        }


        public void SavePage(Page model)
        {
            var sites = SiteDataProvider.GetSites();
            foreach (var site in sites)
            {
                var entity = site.GetPage(model.Id);
                if (entity != null)
                {
                    entity.Template = model.Template;
                    entity.Name = model.Name;
                    entity.Role = model.Role;
                    entity.Keywords = model.Keywords;
                    entity.Description = model.Description;
                    entity.SiteId = model.SiteId;
                    entity.UrlPattern = model.UrlPattern;
                    SiteDataProvider.SaveConfig(sites);

                    break;
                }
            }
        }
    }
}

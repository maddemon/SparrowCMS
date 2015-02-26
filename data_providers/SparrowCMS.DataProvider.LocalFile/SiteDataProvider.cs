using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using SparrowCMS.DataProviders;
using SparrowCMS.Models;

namespace SparrowCMS.DataProvider.Xml
{
    public class SiteDataProvider : ISiteDataProvider
    {
        private string GetConfigPath()
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "configs", "sites.json");
        }

        public List<Models.Site> GetSitesFromConfig()
        {
            var configPath = GetConfigPath();
            var json = File.ReadAllText(configPath);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<List<Models.Site>>(json);
        }

        public List<Models.Site> GetSites()
        {
            var list = GetSitesFromConfig();
            foreach (var site in list)
            {
                foreach (var page in site.Pages)
                {
                    InitPage(page);
                }
            }
            return list;
        }

        private void InitPage(Page page)
        {
            if (page.Template == null) return;
            var templatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, page.Template.FilePath);
            try
            {
                page.Template.Content = File.ReadAllText(templatePath);
            }
            catch
            {
            }
            try
            {
                if (!string.IsNullOrEmpty(page.Template.Layout))
                {
                    var layoutPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, page.Template.Layout);
                    var layoutContent = File.ReadAllText(layoutPath);
                    page.Template.Content = layoutContent.Replace("%body%", page.Template.Content);
                }
            }
            catch { }
        }

        internal void SaveConfig(List<Site> sites)
        {
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(sites);
            File.WriteAllText(GetConfigPath(), json);
        }
    }
}

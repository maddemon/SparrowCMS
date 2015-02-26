using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SparrowCMS.Models
{
    public class Site
    {
        public Site()
        {
            Pages = new List<Page>();
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public string[] Domains { get; set; }

        public List<Page> Pages { get; set; }

        public Page GetPage(string pageIdOrRequestPath)
        {
            return Pages.FirstOrDefault(p => p.IsMatch(pageIdOrRequestPath) || p.Id == pageIdOrRequestPath);
        }

    }
}

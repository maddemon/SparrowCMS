using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SparrowCMS.Base
{
    public class Site
    {
        public Site()
        {
            Enabled = true;
        }

        public int SiteId { get; set; }

        public string Name { get; set; }

        public string[] Domains { get; set; }

        public string Copyrights { get; set; }

        public string Version { get; set; }

        public bool Enabled { get; set; }

        public string Keywords { get; set; }

        public string Description { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SparrowCMS.DataProviders;

namespace SparrowCMS.DataProvider.File
{
    internal class SiteDataProvider : ISiteDataProvider
    {
        public List<Models.Site> GetSites()
        {
            return new List<Models.Site>();
        }
    }
}

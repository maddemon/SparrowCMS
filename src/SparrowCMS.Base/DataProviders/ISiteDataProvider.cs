using SparrowCMS.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SparrowCMS.Core.DataProviders
{
    public interface ISiteDataProvider
    {
        IEnumerable<Site> GetSites();
    }
}

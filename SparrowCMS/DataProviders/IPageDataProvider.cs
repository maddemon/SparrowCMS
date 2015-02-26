using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SparrowCMS.Models;

namespace SparrowCMS.DataProviders
{
    public interface IPageDataProvider
    {
        List<Page> GetPages(Site site);

        Page GetPage(string id);

        void SavePage(Page model);
    }
}

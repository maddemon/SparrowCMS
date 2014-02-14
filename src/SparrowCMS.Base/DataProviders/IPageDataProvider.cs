using SparrowCMS.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SparrowCMS.Core.DataProviders
{
    public interface IPageDataProvider
    {
        IEnumerable<Page> GetPages();
        void AddPage(Page page);
    }
}

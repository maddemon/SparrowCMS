using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SparrowCMS.DataProvider
{
    public interface IPageDataProvider
    {
        IEnumerable<Page> GetPages();
        void AddPage(Page page);
    }
}

using SparrowCMS.Core.DataProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SparrowCMS.DAL.SqlServer
{
    public class PageDataProvider : IPageDataProvider
    {

        public IEnumerable<Core.Models.Page> GetPages()
        {
            using (var db = DbHelper.GetDbContext())
            {
                return db.Pages.ToList();
            }
        }

        public void AddPage(Core.Models.Page page)
        {
            using (var db = DbHelper.GetDbContext())
            {
                db.Pages.Add(page);
                db.SaveChanges();
            }
        }
    }
}

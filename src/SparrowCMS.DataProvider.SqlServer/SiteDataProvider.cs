using SparrowCMS.Core.DataProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SparrowCMS.DAL.SqlServer
{
    public class SiteDataProvider : ISiteDataProvider
    {
        public List<Core.Models.Site> GetSites()
        {
            using (var db = DbHelper.GetDbContext())
            {
                return db.Sites.ToList();
            }
        }
    }
}

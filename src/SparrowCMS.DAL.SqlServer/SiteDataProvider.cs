using SparrowCMS.Base;
using SparrowCMS.Base.IDataProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SparrowCMS.DAL.SqlServer
{
    public class SiteDataProvider : ISiteDataProvider
    {
        public IEnumerable<Base.Site> GetSites()
        {
            using (var db = DbHelper.GetDataContext())
            {
                return db.Sites.Where(e => !e.Deleted).Select(e => new Base.Site
                {
                    Domains = string.IsNullOrEmpty(e.Domains) ? null : e.Domains.Split(','),
                    Id = e.Id,
                    Name = e.Name,

                });
            }
        }
    }
}

using SparrowCMS.Base.IDataProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SparrowCMS.DAL.SqlServer
{
    public class PageDataProvider : IPageDataProvider
    {
        public IEnumerable<Base.Page> GetPages()
        {
            using (var db = DbHelper.GetDataContext())
            {
                return db.Pages.Where(e => !e.Deleted)
                    .Join(db.Templates, e => e.TemplateId, e => e.Id, (p, t) => new Base.Page
                {
                    Template = new Base.Template { Content = t.Content, Name = t.Name },
                    Title = p.Title,
                    Name = p.Name,
                    Description = p.Description,
                    Keywords = p.Keywords,
                    UrlRoute = new Base.UrlRoute(p.UrlPattern),
                }).ToList();
            }
        }
    }
}

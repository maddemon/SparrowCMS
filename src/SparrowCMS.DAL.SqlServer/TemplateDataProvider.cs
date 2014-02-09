using SparrowCMS.Base.IDataProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SparrowCMS.DAL.SqlServer
{
    public class TemplateDataProvider : ITemplateDataProvider
    {
        public IEnumerable<Base.Template> GetTemplates()
        {
            using (var db = DbHelper.GetDataContext())
            {
                return db.Templates.Where(e => !e.Deleted).Select(e => new Base.Template
                {
                    Content = e.Content,
                    Name = e.Name
                }).ToList();
            }
        }
    }
}

using SparrowCMS.Core.DataProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SparrowCMS.DAL.SqlServer
{
    public class TemplateDataProvider : ITemplateDataProvider
    {
        public IEnumerable<Core.Models.Template> GetTemplates()
        {
            using (var db = DbHelper.GetDbContext())
            {
                return db.Templates.ToList();
            }
        }
    }
}

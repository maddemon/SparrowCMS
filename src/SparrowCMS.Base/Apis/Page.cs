using SparrowCMS.Core.Managers;
using SparrowCMS.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SparrowCMS.Core.Apis
{
    public class Page : APIBase
    {
        public ApiResult Save(Models.Page page, string filePath, string layout)
        {
            if (page == null)
            {
                throw new ArgumentNullException("page");
            }

            page.Template = new Template(filePath, layout);

            PageManager.Save(page);

            return Success();
        }

        public string Delete(string urlPattern)
        {
            throw new NotImplementedException();
        }
    }
}

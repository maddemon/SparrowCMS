using SparrowCMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SparrowCMS.Apis
{
    public class Page : APIBase
    {
        public ApiResult Save(Models.Page page, string template, string layout)
        {
            if (page == null)
            {
                throw new ArgumentNullException("page");
            }

            page.Template = new Template
            {
                Content = template,
            };


            return Success();
        }

        public string Delete(string urlPattern)
        {
            throw new NotImplementedException();
        }
    }
}

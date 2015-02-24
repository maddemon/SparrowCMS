using SparrowCMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SparrowCMS.Apis
{
    public class Page : ApiBase
    {
        public ApiResult Save(Models.Page page, Template template)
        {
            if (page == null)
            {
                throw new ArgumentNullException("page");
            }

            page.Template =template;


            return Success();
        }

        public string Delete(string urlPattern)
        {
            throw new NotImplementedException();
        }
    }
}

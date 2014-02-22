using SparrowCMS.Core.Managers;
using SparrowCMS.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SparrowCMS.Core.Apis
{
    public class Page : IApi
    {
        public ApiResult Save(Models.Page model)
        {
            PageManager.Save(model);
            return new ApiResult();
        }

        public string Delete(string urlPattern)
        {
            throw new NotImplementedException();
        }
    }
}

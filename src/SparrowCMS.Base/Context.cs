using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace SparrowCMS.Base
{
    public class Context
    {
        public Site Site { get; set; }

        public Page CurrentPage { get; set; }

        public HttpContext HttpContext { get; set; }

        public RouteData RouteData { get; set; }

        public static Context Current;

        public Context(HttpContext context)
        {
            HttpContext = context;

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using SparrowCMS.Base.Managers;

namespace SparrowCMS.Base
{
    public class Context
    {
        public Site Site { get; set; }

        public Page CurrentPage { get; set; }

        public HttpContext HttpContext { get; set; }

        public static Context Current = new Context();

        private Context()
        {
        }

        public static void Init(HttpContext context)
        {
            //set current site
            var site = SiteManager.GetCurrentSite(context.Request.Url.Host);
            if (site == null)
            {
                throw new Exception("SITE NOT FOUND!");
            }

            Current.Site = site;

            //set current page
            var page = PageManager.GetCurrentPage(site, context);
            if (page == null)
            {
                throw new HttpException(404, "PAGE NOT FOUND!");
            }

            Current.CurrentPage = page;

            //set httpcontext
            Current.HttpContext = context;
        }
    }
}

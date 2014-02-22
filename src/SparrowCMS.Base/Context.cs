using SparrowCMS.Core.Managers;
using SparrowCMS.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace SparrowCMS.Core
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

        public void Init(HttpContext context)
        {

            //set httpcontext
            Current.HttpContext = context;
#if SingleSite
            Current.Site = new Site();
#else
            //set current site
            var site = SiteManager.GetCurrentSite(context.Request.Url.Host);
            if (site == null)
            {
                throw new Exception("SITE NOT FOUND!");
            }

            Current.Site = site;
#endif
            //set current page
            var page = PageManager.GetCurrentPage(Current.Site, this);
            if (page == null)
            {
                throw new HttpException(404, "PAGE NOT FOUND!");
            }

            Current.CurrentPage = page;
        }

        public IEnumerable<string> GetRequestAllKeys()
        {
            return Current.HttpContext.Request.QueryString.AllKeys.Select(e => e.ToLower()).Concat(Context.Current.HttpContext.Request.Form.AllKeys.Select(e => e.ToLower()));
        }

        public string GetPostData()
        {
            using (var sr = new StreamReader(Context.Current.HttpContext.Request.InputStream))
            {
                return sr.ReadToEnd();
            }
        }

        public string Request(string key)
        {
            return Current.CurrentPage.RouteData[key];
        }
    }
}

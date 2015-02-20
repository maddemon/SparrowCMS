using SparrowCMS.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Collections.Specialized;
using SparrowCMS.Managers;
using SparrowCMS.Exceptions;

namespace SparrowCMS
{
    public class CMSContext
    {
        public Site Site { get; set; }

        public Page Page { get; set; }

        public NameValueCollection RouteData { get; set; }

        public Document ViewData { get; set; }

        public HttpContext HttpContext { get; set; }

        public static CMSContext Current = new CMSContext();

        private readonly CoreManager Core = CoreManager.GetInstance();

        private CMSContext()
        {
        }

        public void Init(HttpContext context)
        {

            //set httpcontext
            Current.HttpContext = context;
            Current.Site = Core.SiteManager.GetSite(context.Request.Url.Host);
            if (Current.Site == null)
            {
                throw new SiteNotFoundException();
            }
            //set current page
            var page = Core.PageManager.GetPage(Current.Site, context.Request.Url.AbsolutePath);
            if (page == null)
            {
                throw new PageNotFoundException();
            }

            page.Init(Current);

            Current.Page = page;
            RouteData = page.UrlRoute.GetRouteData(context);
        }

        //public IEnumerable<string> GetRequestAllKeys()
        //{
        //    return HttpContext.Request.QueryString.AllKeys.Select(e => e.ToLower()).Concat(HttpContext.Request.Form.AllKeys.Select(e => e.ToLower()));
        //}

        //public string GetPostData()
        //{
        //    using (var sr = new StreamReader(HttpContext.Request.InputStream))
        //    {
        //        return sr.ReadToEnd();
        //    }
        //}

        //public string Request(string key)
        //{
        //    return Current.RouteData[key];
        //}
    }
}

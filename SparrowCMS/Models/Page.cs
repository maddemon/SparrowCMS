using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace SparrowCMS.Models
{
    public class Page
    {
        public string Id { get; set; }

        public string SiteId { get; set; }

        public string Name { get; set; }

        public Role Role { get; set; }

        public string Keywords { get; set; }

        public string Description { get; set; }

        private string _urlPattern;
        public string UrlPattern
        {
            get { return _urlPattern; }
            set
            {
                _urlPattern = value;
                if (string.IsNullOrEmpty(_urlPattern))
                {
                    _urlRegex = null;
                }
                else
                {
                    _urlRegex = new Regex(_urlPattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);
                }
            }
        }

        private Regex _urlRegex;

        public bool IsMatch(string requestPath)
        {
            return _urlRegex == null ? false : _urlRegex.IsMatch(requestPath);
        }

        public NameValueCollection GetRouteData(HttpContextBase context)
        {
            var data = new NameValueCollection();
            foreach (Match m in _urlRegex.Matches(context.Request.Path))
            {
                foreach (var name in _urlRegex.GetGroupNames())
                {
                    data.Add(name, m.Groups[name].Value);
                }
            }

            foreach (var key in context.Request.QueryString.AllKeys)
            {
                data.Add(key, context.Request.QueryString[key]);
            }

            foreach (var key in context.Request.Form.AllKeys)
            {
                data.Add(key, context.Request.Form[key]);
            }

            return data;
        }

        public Template Template { get; set; }

        public OutputCache OutputCache { get; set; }

        //public UrlRoute UrlRoute { get; set; }

        /// <summary>
        /// 默认输出Template对象替换后的内容
        /// </summary>
        /// <returns></returns>
        public virtual string GetReplacedContext()
        {
            return Template.GetReplacedContent();
        }

        //public NameValueCollection GetRouteData(HttpContext context)
        //{
        //    return UrlRoute.GetRouteData(context);
        //}

        //private bool _initialized;
        //internal void Init(CMSContext context)
        //{
        //    if (_initialized)
        //    {
        //        return;
        //    }

        //    context.RouteData = UrlRoute.GetRouteData(context.HttpContext);

        //    _initialized = true;
        //}
    }
}

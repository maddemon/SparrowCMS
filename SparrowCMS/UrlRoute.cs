using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace SparrowCMS
{
    public class UrlRoute
    {
        private Regex _urlRegex;

        private string _url;
        public string Url
        {
            get
            {
                return _url;
            }
            set
            {
                _url = value;
                _urlRegex = new Regex(_url, RegexOptions.IgnoreCase | RegexOptions.Compiled);
            }
        }

        public bool IsMatch(string requestPath)
        {
            return _urlRegex.IsMatch(requestPath);
        }

        /// <summary>
        /// 需要从Path\QueryString\Form里获取数据
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
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
                data.Add(key, context.Request.QueryString[key]);
            }

            return data;
        }
    }
}

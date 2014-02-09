using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace SparrowCMS.Base
{
    public class UrlRoute
    {

        private static Regex _parameterRegex = new Regex(@"\(\?\<(?<key>\w+)\>|((?<key>\w+)=[^=]+)", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        private Regex _urlRegex;
        private string _pattern;
        public UrlRoute(string urlPattern)
        {
            _pattern = urlPattern;
            _urlRegex = new Regex(_pattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);
        }

        private IEnumerable<string> GetParameterNames()
        {
            var matchs = _parameterRegex.Matches(_pattern);
            if (matchs == null || matchs.Count == 0)
            {
                throw new Exception("urlpattern is error");
            }
            foreach (Match m in matchs)
            {
                yield return m.Groups["key"].Value;
            }
        }

        public RouteData GetRouteData(HttpContext context)
        {
            var absolutePath = context.Request.Url.AbsolutePath;

            var routeData = new RouteData();

            var match = _urlRegex.Match(absolutePath);

            if (match == null)
            {
                throw new Exception(string.Format("{0} does not match {1}", absolutePath, _pattern));
            }

            var parameterNams = GetParameterNames();

            foreach (var name in GetParameterNames())
            {
                var group = match.Groups[name];
                routeData.Add(name, group == null ? context.Request.QueryString[name] : match.Groups[name].Value);
            }

            return routeData;
        }

        public bool IsMatch(string absolutePath)
        {
            return _urlRegex.IsMatch(absolutePath);
        }
    }
}

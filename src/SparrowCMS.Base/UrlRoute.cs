using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace SparrowCMS.Core
{
    public class UrlRoute
    {

        private static Regex _parameterRegex = new Regex(@"\(\?\<(?<key>\w+)\>|((?<key>\w+)=[^=]+)", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        private Regex _urlRegex;
        private string _pattern;
        private IEnumerable<string> _parameterNames;
        public UrlRoute(string urlPattern)
        {
            _pattern = urlPattern;
            _urlRegex = new Regex(_pattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);
            _parameterNames = GetParameterNames();
        }

        private IEnumerable<string> GetParameterNames()
        {
            var matchs = _parameterRegex.Matches(_pattern);
            if (matchs != null && matchs.Count > 0)
            {
                foreach (Match m in matchs)
                {
                    yield return m.Groups["key"].Value;
                }
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

            foreach (var name in _parameterNames)
            {
                var group = match.Groups[name];
                routeData.Add(name, group == null ? context.Request.QueryString[name] : match.Groups[name].Value);
            }

            return routeData;
        }

        public bool IsMatch(string absolutePath)
        {
            if (_parameterNames == null || _parameterNames.Count() == 0)
            {
                return absolutePath.StartsWith(_pattern);
            }
            return _urlRegex.IsMatch(absolutePath);
        }
    }
}

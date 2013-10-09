using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SparrowCMS.Base
{
    public class UrlRoute
    {
        public string Pattern { get; set; }

        public List<UrlParameter> UrlParameters { get; set; }

        private Regex GetRegex()
        {
            return Cache<Regex>.GetOrSet(Pattern, () =>
            {
                var regexPattern = Pattern;
                foreach (var p in UrlParameters)
                {
                    regexPattern = regexPattern.Replace("{" + p.Name + "}", string.Format("(?<{0}>{1})", p.Name, p.Pattern));
                }

                return new Regex(regexPattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);
            });
        }

        public RouteData GetRouteData(string absolutePath)
        {
            var routeData = new RouteData();

            var match = GetRegex().Match(absolutePath);

            if (match == null)
            {
                throw new Exception(string.Format("{0} does not match {1}", absolutePath, Pattern));
            }

            foreach (var p in UrlParameters)
            {
                routeData.Add(p.Name, match.Groups[p.Name].Value);
            }

            //filter unknown querystring?cookies?forms?

            return routeData;
        }
    }
}

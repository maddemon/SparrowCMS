using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SparrowCMS.Models
{
    public class Page
    {
        public string Id { get; set; }

        public string SiteId { get; set; }

        public string Title { get; set; }

        public Role Role { get; set; }

        public string Keywords { get; set; }

        public string Description { get; set; }

        public string UrlPattern { get; set; }

        public Template Template { get; set; }

        public OutputCache OutputCache { get; set; }

        public UrlRoute UrlRoute { get; set; }

        /// <summary>
        /// 默认输出Template对象替换后的内容
        /// </summary>
        /// <returns></returns>
        public virtual string GetReplacedContext()
        {
            return Template.GetReplacedContent();
        }

        private bool _initialized;
        internal void Init(CMSContext context)
        {
            if (_initialized)
            {
                return;
            }

            context.RouteData = UrlRoute.GetRouteData(context.HttpContext);

            _initialized = true;
        }
    }
}

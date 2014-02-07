using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SparrowCMS.Base
{
    public class Page
    {
        private bool _initialized;

        public void Init()
        {
            if (_initialized)
            {
                return;
            }
            //find all label's template content
            Template.Init();

            _initialized = true;
        }

        public PageTemplate Template { get; set; }

        public OutputCache OutputCache { get; set; }

        public UrlRoute UrlRoute { get; set; }

        public RouteData RouteData { get; set; }

        public string GetReplacedContext()
        {
            return Template.GetReplacedContent();
        }
    }
}

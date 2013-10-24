using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SparrowCMS.Base
{
    public class Page : PageBase
    {
        public bool Initialized { get; set; }

        public void Init()
        {
            if (Initialized)
            {
                return;
            }
            //find all label's template content
            foreach (var template in Templates)
            {
                template.Init();
            }

            Initialized = true;
        }

        public OutputCache OutputCache { get; set; }

        public UrlRoute UrlRoute { get; set; }

        public RouteData RouteData { get; set; }
    }
}

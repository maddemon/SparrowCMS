using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace SparrowCMS.Core
{
    public class PageHandlerFactory : IHttpHandlerFactory
    {
        public IHttpHandler GetHandler(HttpContext context, string requestType, string url, string pathTranslated)
        {
            return new PageHandler();
        }

        public void ReleaseHandler(IHttpHandler handler)
        {
        }
    }
}

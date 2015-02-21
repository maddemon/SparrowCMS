using SparrowCMS.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Diagnostics;

namespace SparrowCMS
{
    public class CMSHttpModule : IHttpModule, System.Web.SessionState.IReadOnlySessionState
    {
        public void Dispose()
        {
        }

        public void Init(HttpApplication context)
        {
            context.PreRequestHandlerExecute += new EventHandler(Application_BeginRequest);
            context.Error += new EventHandler(Application_Error);
        }

        private void Application_BeginRequest(Object source, EventArgs e)
        {
            var application = (HttpApplication)source;
            var context = application.Context;
            var url = context.Request.Url.AbsoluteUri;
            Trace.WriteLine(url);
            //创建自定义的HttpHandler处理请求
            context.Handler = new PageHandler();
        }

        private void Application_Error(Object source, EventArgs e)
        {

        }
    }
}

using SparrowCMS.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace SparrowCMS.Core
{
    public class CMSHttpModule : IHttpModule, System.Web.SessionState.IReadOnlySessionState
    {
        public void Dispose()
        {
        }

        public void Init(HttpApplication context)
        {
            //context.BeginRequest += new EventHandler(Application_BeginRequest);
            context.PreRequestHandlerExecute += new EventHandler(Application_BeginRequest);
            context.Error += new EventHandler(Application_Error);
        }

        private void Application_BeginRequest(Object source, EventArgs e)
        {
            var application = (HttpApplication)source;
            var context = application.Context;
            if (context.Request.Path == "/favorite.ico" 
                || context.Request.Path.StartsWith("/static/")
                || context.Request.Path.StartsWith("/html/")
                )
            {
                return;
            }
            else
            {
                context.Handler = new PageHandler();
            }
        }

        private void Application_Error(Object source, EventArgs e)
        {

        }
    }
}

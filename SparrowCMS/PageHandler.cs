using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace SparrowCMS
{
    public class PageHandler : IHttpAsyncHandler , System.Web.SessionState.IRequiresSessionState
    {
        private void ProcessContext(HttpContext context)
        {
            CMSContext.Current.Init(context);
            context.Response.Write(CMSContext.Current.Page.GetReplacedContext());
        }

        private ProcessContextDelegate _delegate;
        private delegate void ProcessContextDelegate(HttpContext context);

        public IAsyncResult BeginProcessRequest(HttpContext context, AsyncCallback cb, object extraData)
        {
            _delegate = new ProcessContextDelegate(ProcessContext);
            return _delegate.BeginInvoke(context, cb, extraData);
        }

        public void EndProcessRequest(IAsyncResult result)
        {
            _delegate.EndInvoke(result);
        }

        public bool IsReusable
        {
            get { return true; }
        }

        public void ProcessRequest(HttpContext context)
        {
            ProcessContext(context);
        }
    }
}

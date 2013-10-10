using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace SparrowCMS.Base
{
    public class PageHandler : IHttpAsyncHandler , System.Web.SessionState.IRequiresSessionState
    {

        public IAsyncResult BeginProcessRequest(HttpContext context, AsyncCallback cb, object extraData)
        {
            throw new NotImplementedException();
        }

        public void EndProcessRequest(IAsyncResult result)
        {
            throw new NotImplementedException();
        }

        public bool IsReusable
        {
            get { throw new NotImplementedException(); }
        }

        public void ProcessRequest(HttpContext context)
        {
            throw new NotImplementedException();
        }
    }
}

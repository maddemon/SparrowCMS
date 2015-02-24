using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace SparrowCMS
{
#if DEBUG
    public class PageHandler : IHttpHandler, System.Web.SessionState.IRequiresSessionState
#else
    public class PageHandler : IHttpAsyncHandler, System.Web.SessionState.IRequiresSessionState
#endif
    {
        private void ProcessContext(HttpContext context)
        {
            //初始化该次请求的Context
            CMSContext.Current.Init(new HttpContextWrapper(context));
            //输出当前请求页面替换后的内容
            //TODO:如果生成静态文件以后需要考虑输出静态文件的相关处理(1判断有效期  2生成和读取文件)
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

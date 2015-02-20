using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Moq;

namespace SparrowCMS.Test
{
    public class HttpContextMock
    {
        public static HttpContextBase FakeHttpContext(string url, string postData = "")
        {
            var context = new Mock<HttpContextBase>();
            var request = new Mock<HttpRequestBase>();
            var response = new Mock<HttpResponseBase>();
            request.Setup(r => r.ApplicationPath).Returns("~/");

            var uri = new Uri(url);
            request.Setup(r => r.Url).Returns(uri);
            request.Setup(r => r.Path).Returns(uri.AbsolutePath);

            request.Setup(r => r.QueryString).Returns(HttpUtility.ParseQueryString(uri.Query));

            request.Setup(r => r.Form).Returns(HttpUtility.ParseQueryString(postData));

            context.Setup(c => c.Request).Returns(request.Object);
            context.Setup(c => c.Response).Returns(response.Object);

            return context.Object;
        }
    }
}

using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SparrowCMS.Base.Parsers;

namespace SparrowCMS.Test
{
    [TestClass]
    public class LabelParserTest
    {
        [TestMethod]
        public void TestSelfCloseLabel()
        {
            var templateContent = @"{System name='SiteName' /}";

            var label = LabelParser.Parse(templateContent).FirstOrDefault();

            Assert.AreNotEqual(null, label);
            Assert.AreEqual("System", label.LabelName);
            var parameter = label.Parameters.FirstOrDefault();
            Assert.AreNotEqual(null,parameter);
            Assert.AreEqual("name", parameter.Name);
            Assert.AreEqual("SiteName", parameter.Value);
        }

        [TestMethod]
        public void TestNormalLabel()
        {
            var templateContent = @"
             {Article.List top=10}
                 <a href=""@CategoryId"">@Category.Name</a>
                 <a href=""@id"">@(title maxlength=20) @(posttime dateformat=""yyyy-MM-dd"")</a>
             {/Article.List}";

            var label = LabelParser.Parse(templateContent).FirstOrDefault();

            Assert.AreNotEqual(null, label);
            Assert.AreEqual("Article.List", label.LabelName);
        }

        [TestMethod]
        public void TestNestLabel()
        {
            var templateContent = @"
         {PageLink pageSize=50 recordCount=@RecordCount}
             <a href=""page/1"">首页</a> | <a href=""page/@prev"">上一页</a>
                 {PageLink.List size=10}
                     <a href=""page/@page"">@page</a>
                 {/PageLink.List}
             <a href=""page/@next"">下一页</a> | <a href=""page/@end"">尾页</a>
         {/PageLink}
";
            var label = LabelParser.Parse(templateContent).FirstOrDefault();

            Assert.AreNotEqual(null, label);
            Assert.AreEqual("PageLink", label.LabelName);

            var pagelistLabel = label.InnerLables.FirstOrDefault();
            Assert.AreNotEqual(null, pagelistLabel);
        }
    }
}

using System.Linq;
using SparrowCMS.Core.Parsers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using SparrowCMS.Core;
using System.Collections.Generic;

namespace SparrowCMS.Test
{
    
    
    /// <summary>
    ///This is a test class for LabelDescriptionParserTest and is intended
    ///to contain all LabelDescriptionParserTest Unit Tests
    ///</summary>
    [TestClass()]
    public class LabelDescriptionParserTest
    {
        /// <summary>
        ///A test for Parse
        ///</summary>
        [TestMethod()]
        public void ParseSelfClosedLabelDescriptionTest()
        {
            var templateContent = @"{System name='SiteName' /}";
            var descs = LabelDescriptionParser.Parse(templateContent);
            var labelDescription = descs.FirstOrDefault();
            Assert.AreEqual("System", labelDescription.LabelName);
        }

        [TestMethod()]
        public void ParseNormalLabelDescriptionTest()
        {
            var templateContent = @"
             {Article.List top=10}
                 <a href=""@CategoryId"">@Category.Name</a>
                 <a href=""@id"">@(title maxlength=20) @(posttime dateformat=""yyyy-MM-dd"")</a>
             {/Article.List}";

            var labelDescription = LabelDescriptionParser.Parse(templateContent).FirstOrDefault();

            Assert.AreEqual("Article.List", labelDescription.LabelName);

        }

        [TestMethod()]
        public void ParseNestedLabelDescriptionTest()
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
            var labelDescription = LabelDescriptionParser.Parse(templateContent).FirstOrDefault();

            Assert.AreEqual("PageLink", labelDescription.LabelName);
        }
    }
}

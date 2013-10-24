using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SparrowCMS.Base.Parsers;

namespace SparrowCMS.Test
{
    [TestClass]
    public class FieldParserTest
    {
        [TestMethod]
        public void TestSimpleField()
        {
            var labelName = "Article.List";

            var templateContent = @"@title";

            var field = FieldParser.Parse(labelName, templateContent).FirstOrDefault();

            Assert.AreNotEqual(null, field);
            Assert.AreEqual("title", field.Name);
        }

        [TestMethod]
        public void TestCustomField()
        {
            var labelName = "PageLink";
            var templateContent = "@next";

            var field = FieldParser.Parse(labelName, templateContent).FirstOrDefault();

            Assert.AreNotEqual(null, field);
            Assert.AreEqual("next", field.Name);

            Assert.AreEqual("SparrowCMS.Base.Labels.PageLink.Fields.Next", field.GetType().FullName);
        }
    }
}

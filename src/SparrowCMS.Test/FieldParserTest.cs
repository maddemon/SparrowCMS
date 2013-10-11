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

            var field = FieldParser.Parse(labelName, templateContent);

            Assert.AreNotEqual(null, field);
            Assert.AreEqual("title", field.Name);
        }

        [TestMethod]
        public void TestCustomField()
        {
            var labelName = "PageLink";
            var templateContent = "@prev";

            var field = FieldParser.Parse(labelName, templateContent);

            Assert.AreNotEqual(null, field);
            Assert.AreEqual("prev", field.Name);

            Assert.AreEqual("SparrowCMS.Base.Labels.PageLink.Fields.Prev", field.GetType().FullName);
        }
    }
}

using System.Linq;
using SparrowCMS.Core.Parsers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using SparrowCMS.Core;
using System.Collections.Generic;

namespace SparrowCMS.Test
{
    
    
    /// <summary>
    ///This is a test class for FieldDescriptionParserTest and is intended
    ///to contain all FieldDescriptionParserTest Unit Tests
    ///</summary>
    [TestClass()]
    public class FieldDescriptionParserTest
    {
        /// <summary>
        ///A test for Parse
        ///</summary>
        [TestMethod()]
        public void ParseSimpleFieldTest()
        {
            var labelName = "Article.List";

            var templateContent = @"@title";

            var fieldDescription = FieldDescriptionParser.Parse(labelName, templateContent).FirstOrDefault();

            Assert.AreEqual("title", fieldDescription.FieldName);

        }

        [TestMethod()]
        public void ParseCustomFieldTest()
        {
            var labelName = "PageLink";
            var templateContent = "@next";

            var fieldDescription = FieldDescriptionParser.Parse(labelName, templateContent).FirstOrDefault();

            Assert.AreEqual("next", fieldDescription.FieldName);
        }

        [TestMethod()]
        public void ParseHasAttributeFieldTest()
        {
            var labelName = "Article.List";

            var templateContent = @"@(title maxLength=20)";

            var fieldDescription = FieldDescriptionParser.Parse(labelName, templateContent).FirstOrDefault();
            var attribute = fieldDescription.Attributes.FirstOrDefault();
            Assert.AreEqual("title", fieldDescription.FieldName);
            Assert.AreEqual("maxlength", attribute.Name.ToLower());
        }
    }
}

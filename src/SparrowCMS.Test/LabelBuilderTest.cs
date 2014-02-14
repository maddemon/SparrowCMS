using System.Linq;
using SparrowCMS.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using SparrowCMS.Core.Parsers;

namespace SparrowCMS.Test
{
    
    
    /// <summary>
    ///This is a test class for LabelBuilderTest and is intended
    ///to contain all LabelBuilderTest Unit Tests
    ///</summary>
    [TestClass()]
    public class LabelBuilderTest
    {
        /// <summary>
        ///A test for Build
        ///</summary>
        [TestMethod()]
        public void BuildTest()
        {
            var templateContent = @"{System name='SiteName' /}";

            var desc = LabelDescriptionParser.Parse(templateContent).FirstOrDefault();

            var label = LabelBuilder.Build(desc);

            Assert.AreEqual("System", label.GetType().Name);

        }
    }
}

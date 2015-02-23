using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SparrowCMS.Labels.Shared.Functions;
using SparrowCMS.Parsers;

namespace SparrowCMS.Test
{
    [TestClass]
    public class LabelParserTest
    {
        [TestInitialize]
        public void InitTest()
        {
            var httpcontext = HttpContextMock.FakeHttpContext("http://localhost/test");
            CMSContext.Current.Init(httpcontext);
        }

        [TestMethod]
        public void test_self_closed_template()
        {
            var template = @"{System name='test' /}";
            var labels = LabelDescriptorParser.FindAll(template);
            var systemLabel = labels[0];

            Assert.AreEqual("System", systemLabel.LabelName);
            var p1 = systemLabel.ParameterDescriptors["name"];
            Assert.AreEqual("test", p1.RawValue);
        }

        [TestMethod]
        public void test_has_innerhtml_template()
        {
            var template = @"
{Article.List CategoryId='Url(cid)'}
    <a href='list?cid=@CategoryId'>@(Title maxlength=20)</a>
{/Article.List}";
            var labels = LabelDescriptorParser.FindAll(template);
            var label = labels[0];

            var parameter = label.ParameterDescriptors.First().Value ;
            Assert.AreEqual(typeof(Url), parameter.Function.GetType());

            Assert.AreEqual("Article.List", label.LabelName);
            var field1 = label.FieldDescriptors[0];
            Assert.AreEqual("CategoryId", field1.FieldName);

            var field2 = label.FieldDescriptors[1];
            Assert.AreEqual(typeof(MaxLength), field2.Functions.First().GetType());
            
        }
    }
}

using SparrowCMS.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace SparrowCMS.Test
{


    /// <summary>
    ///This is a test class for ApiFactoryTest and is intended
    ///to contain all ApiFactoryTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ApiFactoryTest
    {

        /// <summary>
        ///A test for Invoke
        ///</summary>
        [TestMethod()]
        public void InvokeTest()
        {
            Context.Current.CurrentPage = new Core.Models.Page
            {
                RouteData = new RouteData()
            };
            var result = ApiFactory.Invoke(null, "Page", "Save", null);
            Assert.AreNotEqual(null, result);
        }
    }
}

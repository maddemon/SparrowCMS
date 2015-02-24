using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SparrowCMS.Test
{
    [TestClass]
    public class FactoryTest
    {
        [TestMethod]
        public void Test_find_label_in_all_type()
        {
            var ass = Assembly.LoadFrom(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SparrowCMS.dll"));
            foreach (var type in ass.GetTypes())
            {
                if (type.FullName.Contains("Labels"))
                {
                    Trace.WriteLine(type.FullName);
                }
            }
        }
    }
}

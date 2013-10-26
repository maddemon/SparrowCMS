using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SparrowCMS.Base
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class NameAttribute : Attribute
    {
        public string Name { get; private set; }

        public NameAttribute(string name)
        {
            Name = name;
        }
    }
}

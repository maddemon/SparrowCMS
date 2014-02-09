using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SparrowCMS.Base
{
    public class Site
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string[] Domains { get; set; }

    }
}

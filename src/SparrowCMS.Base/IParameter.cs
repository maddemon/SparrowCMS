using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SparrowCMS.Base
{
    public interface IParameter
    {
        string Name { get; set; }

        string Value { get; set; }
    }
}

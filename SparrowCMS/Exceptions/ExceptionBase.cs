using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SparrowCMS.Exceptions
{
    public class ExceptionBase : Exception
    {
        public int Code { get; set; }
    }
}

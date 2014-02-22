using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SparrowCMS.Core.Models
{
    public class ApiResult
    {
        public bool Result { get; set; }

        public string Message { get; set; }

        public object Data { get; set; }
    }
}

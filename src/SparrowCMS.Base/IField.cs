using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SparrowCMS.Base
{
    public interface IField
    {
        string TemplateContent { get; set; }
        string GetValue(object model);
    }
}

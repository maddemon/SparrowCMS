using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SparrowCMS
{
    public interface IField
    {
        string GetReplacedContent(Document doc);

        string TemplateContent { get; set; }
    }

}

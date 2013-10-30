using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SparrowCMS.Base.Labels.PageLink
{
    [Label("PageLink")]
    public class Default : ILabel
    {
        public string GetReplacedContent(string innerHtml)
        {
            throw new NotImplementedException();
        }
    }
}

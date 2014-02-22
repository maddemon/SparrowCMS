using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SparrowCMS.Core.Labels.Page
{
    public class Save : ILabel
    {

        public Models.Page Data { get; set; }

        public string GetReplacedContent(string innerHtml)
        {
            throw new NotImplementedException();
        }
    }
}

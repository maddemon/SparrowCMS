using SparrowCMS.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SparrowCMS.Core.Labels
{
    public class System : ILabel
    {
        public string Name { get; set; }

        public string GetReplacedContent(string innerHtml)
        {
            if (!string.IsNullOrEmpty(Name))
            {
                var data = ShareCache.Get<object>(Name);
                return data == null ? null : data.ToString();
            }
            return null;
        }
    }
}

using SparrowCMS.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SparrowCMS.Labels
{
    public class System : ILabel
    {
        public string Name { get; set; }

        public string GetReplacedContent(string innerHtml)
        {
            if (!string.IsNullOrEmpty(Name))
            {
                var data = RemoteCache.Get<object>(Name);
                return data == null ? null : data.ToString();
            }
            return null;
        }
    }
}

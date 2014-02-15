using SparrowCMS.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SparrowCMS.Core.Labels
{
    public class System : ILabel
    {
        public List<LabelParameter> Parameters { get; set; }

        public string GetReplacedContent(string innerHtml)
        {
            if (Parameters != null)
            {
                var nameParameter = Parameters.FirstOrDefault(e => e.Name == "name");
                if (nameParameter == null)
                {
                    return null;
                }
                var configKey = nameParameter.Value;

                var data = ShareCache.Get<object>(configKey);
                return data == null ? null : data.ToString();
            }
            return null;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SparrowCMS.Labels.Shared.Functions;

namespace SparrowCMS.Labels.Pagination.Fields
{
    public class Prev : IField
    {
        public Format Format { get; set; }

        public string TemplateContent { get; set; }

        public string GetReplacedContent(Document doc)
        {
            var prev = (int)doc["prev"];
            if (prev < 3)
            {
                return string.Empty;
            }
            else
            {
                return Format.GetValue(prev);
            }
        }
    }
}

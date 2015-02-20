using SparrowCMS.Labels.Shared.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SparrowCMS.Labels.Pagination.Fields
{
    public class Prev : Field
    {
        public Format Format { get; set; }

        public override string GetReplacedContent(Document doc)
        {
            var prev = (int)doc["prev"];
            if (prev < 3)
            {
                return string.Empty;
            }
            else
            {
                return Format.ConvertFieldValue(prev);
            }
        }
    }
}

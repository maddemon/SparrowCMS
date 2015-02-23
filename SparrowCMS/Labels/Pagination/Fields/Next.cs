using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SparrowCMS.Labels.Shared.Functions;

namespace SparrowCMS.Labels.Pagination.Fields
{
    public class Next : Field
    {
        public Format Format { get; set; }

        public override string GetReplacedContent(Document doc)
        {
            var next = (int)doc["next"];
            var pageCount = (int)doc["pageCount"];
            if (next >= pageCount - 1)
            {
                return string.Empty;
            }
            else
            {
                return Format.ConvertFieldValue(next);
            }
        }

    }
}

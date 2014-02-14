using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SparrowCMS.Core
{
    public class FieldDescription
    {
        public string LabelName { get; set; }

        public string TemplateContent { get; set; }

        public string FieldName { get; set; }

        public IEnumerable<FieldAttribute> Attributes { get; set; }
    }
}

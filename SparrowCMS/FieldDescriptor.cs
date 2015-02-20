using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SparrowCMS
{
    public class FieldDescriptor
    {
        public string LabelName { get; set; }

        public string TemplateContent { get; set; }

        public string FieldName { get; set; }

        public IEnumerable<FieldFunction> Attributes { get; set; }
    }
}

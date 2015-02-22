using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SparrowCMS
{
    public class FieldDescriptor
    {
        public LabelDescriptor LabelDescriptor { get; set; }

        public string TemplateContent { get; set; }

        public string FieldName { get; set; }

        public IEnumerable<FieldFunction> Functions { get; set; }

        public string GetFieldClassFullName()
        {
            return LabelDescriptor.LabelName + ".Fields." + FieldName;
        }

        public string GetFunctionClassFullName(string functionName)
        {
            return LabelDescriptor.LabelName + ".Functions." + functionName;
        }
    }
}

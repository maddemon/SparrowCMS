using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SparrowCMS
{
    public class LabelDescriptor
    {
        public LabelDescriptor()
        {
            Parameters = new Dictionary<string, LabelParameter>();
            FieldDescriptors = new List<FieldDescriptor>();
            InnerLabelDescriptors = new List<LabelDescriptor>();
        }

        public string ID { get; set; }

        public string LabelName { get; set; }

        public string ClassName { get; set; }

        public string TemplateContent { get; set; }

        public string InnerHtml { get; set; }

        public Dictionary<string, LabelParameter> Parameters { get; set; }

        public IEnumerable<FieldDescriptor> FieldDescriptors { get; set; }

        public IEnumerable<LabelDescriptor> InnerLabelDescriptors { get; set; }
    }
}

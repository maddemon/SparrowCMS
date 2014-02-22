using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SparrowCMS.Core
{
    public class LabelDescription
    {
        public LabelDescription()
        {
            Parameters = new Dictionary<string, Parameter>();
            FieldDescriptions = new List<FieldDescription>();
            InnerLabelDescriptions = new List<LabelDescription>();
        }

        public Guid ID { get; set; }

        public string LabelName { get; set; }

        public string TemplateContent { get; set; }

        public string InnerHtml { get; set; }

        public Dictionary<string, Parameter> Parameters { get; set; }

        public IEnumerable<FieldDescription> FieldDescriptions { get; set; }

        public IEnumerable<LabelDescription> InnerLabelDescriptions { get; set; }
    }
}

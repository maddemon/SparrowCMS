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
            Parameters = new Dictionary<string, LabelParameter>();
            FieldDescriptions = new List<FieldDescription>();
            InnerLabelDescriptions = new List<LabelDescription>();
        }

        public string LabelName { get; set; }

        public string TemplateContent { get; set; }

        public string InnerHtml { get; set; }

        public Dictionary<string, LabelParameter> Parameters { get; set; }

        public IEnumerable<FieldDescription> FieldDescriptions { get; set; }

        public IEnumerable<LabelDescription> InnerLabelDescriptions { get; set; }
    }
}

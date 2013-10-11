using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SparrowCMS.Base
{
    public abstract class LabelBase : ITemplate
    {
        public string LabelName { get; set; }

        public string ActionName { get; set; }

        public string Content { get; set; }

        public abstract string GetReplacedContent();

        public string TemplateContent { get; set; }

        public IEnumerable<Field> Fields { get; set; }

        public IEnumerable<LabelParameter> Parameters { get; set; }

        public IEnumerable<LabelBase> InnerLables { get; set; }
    }
}

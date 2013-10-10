using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SparrowCMS.Base
{
    public abstract class LabelBase : ILabel
    {
        public string LabelName { get; set; }

        public string ActionName { get; set; }

        public string Content { get; set; }

        public List<IField> Fields { get; set; }

        public List<IParameter> Parameters { get; set; }

        public abstract string GetReplacedContent();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SparrowCMS.Labels.Shared
{
    public abstract class ModelLabelBase : ILabel
    {
        protected abstract Document GetData();

        public IEnumerable<Field> Fields { get; set; }

        public virtual string GetReplacedContent(string innerHtml)
        {
            var data = GetData();

            return this.GetReplacedModelContent(innerHtml, data, Fields);
        }
    }
}

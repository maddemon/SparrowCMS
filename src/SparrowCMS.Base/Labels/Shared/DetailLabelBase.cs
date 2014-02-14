using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SparrowCMS.Core.Labels.Shared
{
    public abstract class DetailLabelBase : ILabel
    {
        protected abstract Document GetData();

        public IEnumerable<Field> Fields { get; set; }

        public virtual string GetReplacedContent(string innerHtml)
        {
            var data = GetData();

            foreach (var field in Fields)
            {
                innerHtml = innerHtml.Replace(field.TemplateContent, field.GetReplacedContent(data));
            }

            return innerHtml;
        }
    }
}

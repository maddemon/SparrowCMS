using SparrowCMS.Core.Parsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SparrowCMS.Core.Labels.Shared
{
    public abstract class ModelLabelBase : ILabel
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

            var innerLabelDescriptions = LabelDescriptionParser.Parse(innerHtml);
            if (innerLabelDescriptions != null && innerLabelDescriptions.Count() > 0)
            {
                foreach (var desc in innerLabelDescriptions)
                {
                    var innerLabel = LabelBuilder.Build(desc);
                    innerHtml = innerHtml.Replace(desc.TemplateContent, innerLabel.GetReplacedContent(desc.InnerHtml));
                }
            }
            return innerHtml;
        }
    }
}

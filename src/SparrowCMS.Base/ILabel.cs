using SparrowCMS.Core.Parsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SparrowCMS.Core
{
    public interface ILabel
    {
        string GetReplacedContent(string innerHtml);
    }

    public static class LabelUtils
    {
        public static string GetRelacedModelContent(this ILabel label, string innerHtml, Document model, IEnumerable<Field> fields = null)
        {
            if (fields != null)
            {
                foreach (var field in fields)
                {
                    innerHtml = innerHtml.Replace(field.TemplateContent, field.GetReplacedContent(model));
                }
            }
            
            return label.GetReplacedInnerLabelContent(innerHtml, model);
        }

        public static string GetReplacedInnerLabelContent(this ILabel label, string innerHtml, Document parentModel)
        {
            ////has inner by parameter
            //if (Parameters != null && Parameters.Any(e => e.Name.ToLower() == "inner"))
            //{

            //}
            //处理内嵌标签
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

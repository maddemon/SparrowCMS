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

    //public abstract class Label
    //{
    //    public Guid ID { get; set; }

    //    public IEnumerable<LabelParameter> Parameters { get; set; }

    //    public IEnumerable<Field> Fields { get; set; }

    //    public abstract string GetReplacedContent(string innerHtml);

    //    protected virtual string GetReplacedModelContent(string innerHtml, Document model)
    //    {
    //        if (Fields != null)
    //        {
    //            foreach (var field in Fields)
    //            {
    //                innerHtml = innerHtml.Replace(field.TemplateContent, field.GetReplacedContent(model));
    //            }
    //        }

    //        //if has inner label
    //        //get inner label
    //        var innerLabelDescriptions = LabelDescriptionParser.Parse(innerHtml);
    //        if (innerLabelDescriptions != null && innerLabelDescriptions.Count() > 0)
    //        {
    //            foreach (var desc in innerLabelDescriptions)
    //            {
    //                var innerLabel = LabelBuilder.Build(desc);
    //                innerHtml = innerLabel.GetReplacedContent(innerHtml);
    //            }
    //        }
    //        return innerHtml;
    //    }
    //}
}

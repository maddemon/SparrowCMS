using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SparrowCMS.Base.Parsers;

namespace SparrowCMS.Base
{
    public class PageTemplate : ITemplate
    {
        public PageTemplate()
        {
        }

        public string Name { get; set; }

        public string StaticFilePath { get; set; }

        public string Content { get; set; }

        public IEnumerable<LabelDescription> LabelDescriptions { get; set; }

        public void Init()
        {
            //Labels = LabelParser.Parse(Content);
        }

        public virtual string GetReplacedContent()
        {
            foreach (var desc in LabelDescriptions)
            {
                var label = LabelBuilder.Build(desc);
                var replacedHtml = label.GetReplacedContent(desc.InnerHtml);
                Content = Content.Replace(desc.TemplateContent, replacedHtml);
            }

            return Content;
        }

        //private string GetReplacedInnerHtml(LabelDescription desc)
        //{
        //    if (desc.InnerLabelDescriptions.Count() > 0)
        //    {
        //        foreach (var d in desc.InnerLabelDescriptions)
        //        {
        //            ReplaceInnerHtml(d);
        //        }
        //    }

        //    var label = desc.CreateLabel();
        //    return label.GetReplacedContent(desc.InnerHtml);
        //}
    }
}

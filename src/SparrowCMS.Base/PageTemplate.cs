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
            foreach (var description in LabelDescriptions)
            {
                var label = description.CreateLabel();
                var replacedHtml = label.GetReplacedContent(description.InnerHtml);
                Content = Content.Replace(description.TemplateContent, replacedHtml);
            }

            return Content;
        }
    }
}

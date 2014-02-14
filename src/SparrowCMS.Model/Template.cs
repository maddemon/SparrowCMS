using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SparrowCMS.Model
{
    public class Template
    {
        public int Id { get; set; }

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
    }
}

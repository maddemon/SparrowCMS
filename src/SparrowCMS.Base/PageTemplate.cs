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

        public IEnumerable<LabelBase> Labels { get; set; }

        public void Init()
        {
            Labels = LabelParser.Parse(Content);
        }

        public virtual string GetReplacedContent()
        {
            foreach (var label in Labels)
            {
                Content = Content.Replace(label.Content, label.GetReplacedContent());
            }

            return Content;
        }
    }
}

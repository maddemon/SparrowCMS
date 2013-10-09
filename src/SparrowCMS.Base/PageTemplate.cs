using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SparrowCMS.Base
{
    public class PageTemplate : ITemplate
    {
        public string Name { get; set; }

        public string StaticFilePath { get; set; }

        public string Content { get; set; }

        public List<ILabel> Labels { get; set; }

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

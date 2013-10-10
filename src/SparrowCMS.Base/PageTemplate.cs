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
            Labels = new List<ILabel>();
        }

        public string Name { get; set; }

        public string StaticFilePath { get; set; }

        public string Content { get; set; }

        public List<ILabel> Labels { get; set; }

        public void Init()
        {
            var labelTemplates = TemplateAnalyzer.FindLabelTemplates(Content);
            //replace include template
            foreach (var tmpContent in labelTemplates)
            {
                Labels.Add(LabelParser.Parse(tmpContent));
            }
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

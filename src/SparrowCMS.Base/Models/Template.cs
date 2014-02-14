using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace SparrowCMS.Core.Models
{
    public class Template
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }
        
        public string Content { get; set; }

        [NotMapped]
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

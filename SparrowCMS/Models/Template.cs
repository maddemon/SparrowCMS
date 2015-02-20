using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SparrowCMS.Models
{
    public class Template
    {
        public string Id { get; set; }

        public string Content { get; set; }

        private Lazy<List<LabelDescriptor>> _initLabelDescriptions;

        public List<LabelDescriptor> LabelDescriptions { get { return _initLabelDescriptions.Value; } }

        public Template()
        {
            _initLabelDescriptions = new Lazy<List<LabelDescriptor>>(() =>
            {
                return Parsers.LabelDescriptorParser.Parse(Content).ToList();
            });
        }
        
        public virtual string GetReplacedContent()
        {

            foreach (var desc in LabelDescriptions)
            {
                var label = LabelBuilder.Build(desc);
                if (label == null)
                {
                    continue;
                }
                var replacedHtml = label.GetReplacedContent(desc.InnerHtml);
                Content = Content.Replace(desc.TemplateContent, replacedHtml);
            }

            return Content;
        }

    }
}

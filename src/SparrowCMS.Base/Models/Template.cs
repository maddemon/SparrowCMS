using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Text;

namespace SparrowCMS.Core.Models
{
    public class Template
    {
        public Template(string filePath, string layout = null)
        {
            FilePath = filePath;
            Layout = layout;
        }

        public string Layout { get; set; }

        public string FilePath { get; set; }

        public string Content { get; set; }

        public IEnumerable<LabelDescription> LabelDescriptions { get; set; }

        public void Init()
        {
            var filePath = Path.Combine(Environment.CurrentDirectory, FilePath);
            Content = File.ReadAllText(FilePath);

            if (!string.IsNullOrEmpty(Layout))
            {
                var layoutContent = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, Layout));
                Content = layoutContent.Replace("%body%", Content);
            }

            LabelDescriptions = Parsers.LabelDescriptionParser.Parse(Content);
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

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using SparrowCMS.Attributes;

namespace SparrowCMS.Models
{
    public class Template
    {
        public string FilePath { get; set; }

        public string Layout { get; set; }

        public string Name { get; set; }

        public string Content { get; set; }

        private Lazy<List<LabelDescriptor>> _initLabelDescriptions;

        [DocumentIgnore]
        public List<LabelDescriptor> LabelDescriptions { get { return _initLabelDescriptions.Value; } }

        /// <summary>
        /// Template在实例化时必须指定了Content,并且Content的内容是处理过Layout 和 include后的(由PageManager负责)
        /// </summary>
        public Template()
        {
            _initLabelDescriptions = new Lazy<List<LabelDescriptor>>(() =>
            {
                //延迟创建label类的descriptor 
                return Parsers.LabelDescriptorParser.FindAll(Content);
            });
        }
        
        /// <summary>
        /// 默认遍历所有Label,并把所有Label的内容替换为生成后的数据内容
        /// </summary>
        /// <returns></returns>
        public virtual string GetReplacedContent()
        {
            foreach (var desc in LabelDescriptions)
            {
                //根据所有的label的Descriptor对象,实例化Label对象,并执行Label的GetReplacedContent
                var label = desc.GetLabel();
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

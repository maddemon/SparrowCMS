using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SparrowCMS.Core.Labels.Shared
{
    public abstract class ListLabelBase : ILabel
    {
        private IEnumerable<Document> _datas = null;

        public abstract IEnumerable<Document> GetDatas();

        public IEnumerable<Field> Fields { get; set; }

        protected virtual void InitDatas()
        {
            if (_datas == null)
            {
                _datas = GetDatas();
            }
        }

        public virtual string GetReplacedContent(string innerHtml)
        {
            InitDatas();

            var result = new StringBuilder();

            foreach (var data in _datas)
            {
                foreach (var field in Fields)
                {
                    result.Append(innerHtml.Replace(field.TemplateContent, field.GetReplacedContent(data)));
                }
            }

            return result.ToString();
        }
    }
}

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

        public int RecordCount { get; set; }

        public string PaginationId { get; set; }

        protected virtual void InitDatas()
        {
            if (_datas == null)
            {
                _datas = GetDatas();
            }
        }

        protected virtual int GetRecordCount()
        {
            return RecordCount = RecordCount > 0 ? RecordCount : _datas.Count();
        }

        private void SavePaginationRecordCount()
        {
            if (!string.IsNullOrEmpty(PaginationId))
            {
                var recordCount = GetRecordCount();
                Context.Current.CurrentPage.ViewData[PaginationId] = recordCount;
            }
        }

        public virtual string GetReplacedContent(string innerHtml)
        {
            InitDatas();

            SavePaginationRecordCount();

            var result = string.Empty;
            if (_datas == null) return result;

            foreach (var data in _datas)
            {
                var row = innerHtml;
                foreach (var field in Fields)
                {
                    row = row.Replace(field.TemplateContent, field.GetReplacedContent(data));
                }
                result += row;
            }

            return result.ToString();
        }
    }
}

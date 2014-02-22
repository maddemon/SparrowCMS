using SparrowCMS.Core.Labels.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SparrowCMS.Core.Labels.Pagination
{
    public class List : ListLabelBase
    {
        public int Size { get; set; }

        public int PageCount { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public override IEnumerable<Document> GetDatas()
        {
            var startPage = PageIndex - PageIndex % Size + 1;
            var endPage = PageIndex / Size * PageSize;
            if (endPage == 0) endPage = PageCount;
            var data = new List<Document>();
            for (; startPage <= endPage; startPage++)
            {
                dynamic doc = new Document();
                doc.Index = startPage;
                data.Add(doc);
            }
            return data;
        }
    }
}

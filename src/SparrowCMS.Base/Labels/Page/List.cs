using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SparrowCMS.Core.Labels.Shared;
using SparrowCMS.Core.Managers;
using SparrowCMS.Core.Common;
using SparrowCMS.Core.Attributes;

namespace SparrowCMS.Core.Labels.Page
{
    public class List : ListLabelBase
    {
        public int PageSize { get; set; }

        public int SiteId { get; set; }

        public int Page { get; set; }

        public override IEnumerable<Document> GetDatas()
        {
            var query = PageManager.GetPages(null).AsEnumerable();
            if (!string.IsNullOrEmpty(PaginationId))
            {
                RecordCount = query.Count();
            }

            if (PageSize > 0)
            { 
                if(Page == 0) Page =1;
                query = query.Skip((Page - 1) * PageSize).Take(PageSize);
            }
            return query.Select(e => e.ToDocument());
        }
    }
}

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
        [Parameter]
        public int PageSize { get; set; }

        [Parameter]
        public int SiteId { get; set; }

        public override IEnumerable<Document> GetDatas()
        {
            return PageManager.GetPages(null).Select(e => e.ToDocument());
        }
    }
}

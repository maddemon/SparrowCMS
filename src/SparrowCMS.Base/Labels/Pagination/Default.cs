using SparrowCMS.Core.Labels.Pagination.Fields;
using SparrowCMS.Core.Labels.Shared;
using SparrowCMS.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SparrowCMS.Core.Labels.Pagination
{
    public class Default : ModelLabelBase
    {
        public string Id { get; set; }

        public int PageSize { get; set; }

        [FieldFlag]
        public Prev Prev { get; set; }

        [FieldFlag]
        public Next Next { get; set; }

        protected override Document GetData()
        {
            var data = new Document();
            var page = 1;
            int.TryParse(Context.Current.CurrentPage.RouteData["page"], out page);
            if (page == 0) page = 1;
            data["page"] = page;
            data["next"] = page + 1;
            data["prev"] = page == 1 ? 1 : page - 1;
            data["pagesize"] = PageSize;
            var end = 100;
            var recordCount = Context.Current.CurrentPage.ViewData[Id];
            if (recordCount != null)
            {
                end = (int)recordCount / PageSize + 1;
            }
            data["recordCount"] = recordCount;
            data["PageCount"] = end;
            data["end"] = end;
            return data;
        }
    }
}

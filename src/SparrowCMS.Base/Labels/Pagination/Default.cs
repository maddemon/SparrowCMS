using SparrowCMS.Core.Labels.Shared;
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

        protected override Document GetData()
        {
            var data = new Document();
            var page = 1;
            int.TryParse(Context.Current.CurrentPage.RouteData["page"], out page);
            data["page"] = page == 0 ? 1 : page;
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

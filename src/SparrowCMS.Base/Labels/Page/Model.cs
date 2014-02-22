using SparrowCMS.Core.Labels.Shared;
using SparrowCMS.Core.Managers;
using SparrowCMS.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SparrowCMS.Core.Labels.Page
{
    public class Model : ModelLabelBase
    {
        public string Id { get; set; }

        protected override Document GetData()
        {
            var model = PageManager.GetPages(null).FirstOrDefault(e => e.Id.ToString() == Id) ?? new Models.Page();
            return model.ToDocument();
        }
    }
}

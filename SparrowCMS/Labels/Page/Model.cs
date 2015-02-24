using SparrowCMS.Labels.Shared;
using SparrowCMS.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SparrowCMS.Managers;

namespace SparrowCMS.Labels.Page
{
    public class Model : ModelLabelBase
    {
        public string Id { get; set; }

        private PageManager PageManager = new PageManager();

        protected override Document GetData()
        {
            return PageManager.GetPage(Id).ToDocument();
        }
    }
}

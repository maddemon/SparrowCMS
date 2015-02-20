using SparrowCMS.Labels.Shared;
using SparrowCMS.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SparrowCMS.Labels.Page
{
    public class Model : ModelLabelBase
    {
        public string Id { get; set; }

        protected override Document GetData()
        {
            throw new NotImplementedException();
        }
    }
}

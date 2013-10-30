using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SparrowCMS.Base.Labels.PageLink.Fields
{
    public class Next : Field
    {
        public Next()
        {
            Name = this.GetType().Name;
        }

        public override object GetValue(Document doc)
        {
            return (int)doc["CurrentPage"] + 1;
        }

    }
}

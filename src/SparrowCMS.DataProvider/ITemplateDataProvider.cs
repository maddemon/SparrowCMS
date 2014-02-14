using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SparrowCMS.DataProvider
{
    public interface ITemplateDataProvider
    {
        IEnumerable<Template> GetTemplates();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SparrowCMS.Base
{
    public class FieldDescription
    {
        public string TemplateContent { get; set; }

        public string FieldName { get; set; }

        public IEnumerable<FieldParameter> Parameters { get; set; }

        public Field CreateField()
        {
            throw new NotImplementedException();
        }
    }
}

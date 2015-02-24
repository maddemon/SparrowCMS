using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SparrowCMS.Attributes;
using SparrowCMS.ModelBinders;

namespace SparrowCMS
{
    public class ApiActionParameterDescriptor
    {
        public string ParameterName { get; set; }

        public Type ParameterType { get; set; }

        public CustomModelBinderAttribute BinderAttribute { get; set; }

        public object GetValue()
        {
            if (BinderAttribute != null)
            {
                return BinderAttribute.GetBinder().BindModel(this);
            }
            else
            {
                return DefaultModelBinder.Instance.BindModel(this);
            }
        }
    }
}

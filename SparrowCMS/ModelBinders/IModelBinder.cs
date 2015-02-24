using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SparrowCMS.ModelBinders
{
    public interface IModelBinder
    {
        object BindModel(ApiActionParameterDescriptor descriptor);
    }
}

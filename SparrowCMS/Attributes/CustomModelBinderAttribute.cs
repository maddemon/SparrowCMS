using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SparrowCMS.ModelBinders;

namespace SparrowCMS.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Interface | AttributeTargets.Parameter, AllowMultiple = false, Inherited = false)]
    public abstract class CustomModelBinderAttribute : Attribute
    {
        public abstract IModelBinder GetBinder();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SparrowCMS.ModelBinders;

namespace SparrowCMS.Attributes
{
    public sealed class ModelBinderAttribute : CustomModelBinderAttribute
    {
        public Type BinderType
        {
            get;
            private set;
        }

        public ModelBinderAttribute(Type binderType)
        {
            if (binderType == null)
            {
                throw new ArgumentNullException("binderType");
            }
            if (!typeof(IModelBinder).IsAssignableFrom(binderType))
            {
                throw new ArgumentException("binderType");
            }
            this.BinderType = binderType;
        }

        public override IModelBinder GetBinder()
        {
            try
            {
                return (IModelBinder)Activator.CreateInstance(this.BinderType);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

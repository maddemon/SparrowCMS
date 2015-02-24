using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SparrowCMS.Common;

namespace SparrowCMS.ModelBinders
{
    public class DefaultModelBinder : IModelBinder
    {
        private DefaultModelBinder() { }

        public static readonly DefaultModelBinder Instance = new DefaultModelBinder();
        private static readonly FormBinder FormBinder = new FormBinder();
        public object BindModel(ApiActionParameterDescriptor descriptor)
        {
            var modelName = descriptor.ParameterName;
            var modelType = descriptor.ParameterType;

            //TODO:array

            if (modelType.IsBasicType())
            {
                var value = CMSContext.Current.RouteData[modelName];
                return value.ToValue(modelType);
            }
            else
            {
                return FormBinder.BindModel(descriptor);
            }
        }
    }
}

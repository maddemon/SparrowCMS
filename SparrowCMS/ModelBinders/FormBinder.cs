using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SparrowCMS.Common;

namespace SparrowCMS.ModelBinders
{
    public class FormBinder : IModelBinder
    {
        public object BindModel(ApiActionParameterDescriptor descriptor)
        {
            var request = CMSContext.Current.HttpContext;
            var modelType = descriptor.ParameterType;
            var model = Activator.CreateInstance(modelType);
            foreach (var prop in modelType.GetProperties())
            {
                //TODO:只支持基本类型
                if (prop.PropertyType.IsBasicType())
                {
                    var val = CMSContext.Current.RouteData[prop.Name].ToValue(prop.PropertyType);
                    prop.SetValue(model, val, null);
                }
            }
            return model;
        }
    }
}

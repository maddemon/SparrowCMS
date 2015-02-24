using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SparrowCMS.Attributes;
using SparrowCMS.Common;

namespace SparrowCMS
{
    public class ApiDescriptor
    {
        public ApiDescriptor()
        {
            ActionDescriptors = new List<ApiActionDescriptor>();
        }

        public IApi ApiInstance { get; set; }

        public List<ApiActionDescriptor> ActionDescriptors { get; set; }

        public ApiActionDescriptor FindAction(string actionName)
        {
            var requestKeys = CMSContext.Current.RouteData.AllKeys;
            var list = ActionDescriptors.Where(a => a.ActionName.ToLower() == actionName.ToLower()).OrderByDescending(a => a.Parameters.Count);
            foreach (var action in list)
            {
                var isMatch = true;
                foreach (var p in action.Parameters)
                {
                    if (!p.ParameterType.IsBasicType()) continue;
                    if (!requestKeys.Any(key => key.ToLower() == p.ParameterName.ToLower()))
                    {
                        isMatch = false;
                        break;
                    }
                }

                if (isMatch)
                {
                    return action;
                }
            }
            return null;
        }

        internal static ApiDescriptor Create(IApi apiInstance)
        {
            var apiDesc = new ApiDescriptor
            {
                ApiInstance = apiInstance,
            };
            foreach (var methodInfo in apiInstance.GetType().GetMethods())
            {
                var actionDesc = new ApiActionDescriptor
                {
                    ActionName = methodInfo.Name,
                    ActionMethod = methodInfo,
                };

                foreach (var parameterInfo in methodInfo.GetParameters())
                {
                    var parameterDesc = new ApiActionParameterDescriptor
                    {
                        ParameterName = parameterInfo.Name,
                        ParameterType = parameterInfo.ParameterType,
                        BinderAttribute = (CustomModelBinderAttribute)parameterInfo.ParameterType.GetCustomAttributes(typeof(CustomModelBinderAttribute), false).FirstOrDefault()
                    };

                    actionDesc.Parameters.Add(parameterDesc);
                }
                apiDesc.ActionDescriptors.Add(actionDesc);
            }
            return apiDesc;
        }
    }
}

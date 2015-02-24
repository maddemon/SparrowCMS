using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SparrowCMS
{
    public class ApiActionDescriptor
    {
        public ApiActionDescriptor()
        {
            Parameters = new List<ApiActionParameterDescriptor>();
        }

        public string ActionName { get; set; }

        public MethodInfo ActionMethod { get; set; }

        public List<ApiActionParameterDescriptor> Parameters { get; set; }

        public object InvokeAction(IApi api)
        {
            var pValues = new List<object>();
            foreach (var p in Parameters)
            {
                pValues.Add(p.GetValue());
            }
            return ActionMethod.Invoke(api, pValues.ToArray());
        }
    }
}

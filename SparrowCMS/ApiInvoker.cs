using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SparrowCMS.Exceptions;
using SparrowCMS.Managers;

namespace SparrowCMS
{
    public class ApiInvoker
    {
        private static readonly ApiManager ApiManager = new ApiManager();
        public object Invoke(string pluginName, string apiName, string actionName)
        {
            var api = ApiManager.GetApi(pluginName, apiName);
            if (api != null)
            {
                var action = api.FindAction(actionName);
                if (action != null)
                {
                    return action.InvokeAction(api.ApiInstance);
                }
            }
            throw new PageNotFoundException();
        }
    }
}

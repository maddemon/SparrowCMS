using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using SparrowCMS.Core.Common;

namespace SparrowCMS.Core
{
    public class ApiFactory
    {
        public static object Invoke(string pluginName, string className, string methodName, string dataType)
        {
            var api = GetApiInstance(pluginName, className);
            var method = GetApiMethod(api, methodName);
            return InvokeApi(api, method, dataType);
        }

        private static IApi GetApiInstance(string pluginName, string className)
        {
            var apiInstance = Factory.Instance.GetInstance<IApi>(pluginName, className);
            if (apiInstance == null)
            {
                throw new HttpException(404, "Not found " + className + " API.");
            }
            return apiInstance;
        }

        private static MethodInfo GetApiMethod(IApi api, string methodName)
        {
            var method = api.GetType().GetMethods().FirstOrDefault(e => e.Name.ToLower() == methodName.ToLower());
            if (method == null)
            {
                throw new HttpException(404, "Not found " + method + " API method.");
            }
            return method;
        }

        private static string GetPostData()
        {
            using (var sr = new StreamReader(Context.Current.HttpContext.Request.InputStream))
            {
                return sr.ReadToEnd();
            }
        }

        private static IEnumerable<string> GetRequestAllKeys()
        {
            return Context.Current.CurrentPage.RouteData.AllKeys;
        }

        private static object GetBasicParameterValue(ParameterInfo p)
        {
            object pValue = null;
            if (Context.Current.CurrentPage.RouteData.AllKeys.Contains(p.Name.ToLower()))
            {
                pValue = Context.Current.Request(p.Name).ToValue(p.ParameterType);
            }
            else
            {
                pValue = p.ParameterType.GetDefaultValue();
            }
            return pValue;
        }

        private static object InvokeApi(IApi api, MethodInfo apiMethod, string dataType)
        {
            var parameters = new List<object>();
            var allKeys = GetRequestAllKeys();

            switch (dataType)
            {
                case "json":
                    var data = Newtonsoft.Json.JsonConvert.DeserializeObject<JObject>(GetPostData());
                    foreach (var p in apiMethod.GetParameters())
                    {
                        object pValue = null;
                        if (p.ParameterType.IsBasicType())
                        {
                            pValue = GetBasicParameterValue(p);
                        }
                        else
                        {
                            JToken jValue = null;
                            if (data.TryGetValue(p.Name, out jValue))
                            {
                                try
                                {
                                    pValue = jValue.ToObject(p.ParameterType);
                                }
                                catch
                                {
                                    pValue = p.ParameterType.GetDefaultValue();
                                }
                            }
                            else
                            {
                                pValue = p.ParameterType.GetDefaultValue();
                            }
                        }
                        parameters.Add(pValue);
                    }
                    break;
                case "xml":

                    break;
                default:
                    foreach (var p in apiMethod.GetParameters())
                    {
                        object pValue = p.ParameterType.GetDefaultValue();
                        if (p.ParameterType.IsBasicType())
                        {
                            pValue = GetBasicParameterValue(p);
                        }
                        else
                        {
                            pValue = Activator.CreateInstance(p.ParameterType);
                            var shouldUseNewObject = false;
                            foreach (var prop in p.ParameterType.GetProperties())
                            {
                                //TODO:只支持基本类型
                                if (prop.PropertyType.IsBasicType())
                                {
                                    var val = Context.Current.Request(prop.Name).ToValue(prop.PropertyType);
                                    prop.SetValue(pValue, val, null);
                                    shouldUseNewObject = true;
                                }
                            }
                            if (!shouldUseNewObject)
                            {
                                pValue = null;
                            }
                        }
                        parameters.Add(pValue);
                    }
                    break;
            }
            return apiMethod.Invoke(api, parameters.ToArray());
        }
    }
}

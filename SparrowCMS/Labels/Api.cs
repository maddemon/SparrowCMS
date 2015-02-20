using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Newtonsoft.Json;
using SparrowCMS.Models;

namespace SparrowCMS.Labels
{
    public class Api : ILabel
    {
        public string Action { get; set; }

        public string Version { get; set; }

        public string DataType { get; set; }

        public string Plugin { get; set; }

        private string _apiName;

        private string _methodName;

        private void SetApiNames()
        {
            if (string.IsNullOrEmpty(Action))
            {

            }
            var names = Action.Split('.');
            if (names.Length == 1)
            {
                _apiName = names[0];
                _methodName = "index";
            }
            else if (names.Length == 2)
            {
                _apiName = names[0];
                _methodName = names[1];
            }
            else
            {
                
            }
        }

        public string GetReplacedContent(string innerHtml)
        {
            try
            {
                SetApiNames(); 
                var result = ApiFactory.Invoke(Plugin, _apiName, _methodName, DataType);

                return Newtonsoft.Json.JsonConvert.SerializeObject(result);
            }
            catch (Exception ex)
            {
                //Context.Current.HttpContext.Response.StatusCode = ex.GetStatusCode();
                return Newtonsoft.Json.JsonConvert.SerializeObject(new ApiResult
                {
                    Result = false,
                    Data = ex,
                    Message = ex.Message
                });
            }
        }
    }
}

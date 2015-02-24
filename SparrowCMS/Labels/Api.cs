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
        public Api()
        {
        }

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
                _apiName = Action.Substring(0, Action.LastIndexOf('.'));
                _methodName = names.Last();
            }
        }

        private static ApiBuilder _builder = new ApiBuilder();

        public string GetReplacedContent(string innerHtml)
        {
            SetApiNames();
            var result = _builder.Invoke(Plugin, _apiName, _methodName, DataType);

            return result.ToString();
        }
    }
}

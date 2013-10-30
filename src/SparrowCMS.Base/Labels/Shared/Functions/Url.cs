using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SparrowCMS.Base.Labels.Shared.Functions
{
    public class Url : IFunction
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="originalValue">Url(Id)</param>
        /// <returns></returns>
        public string GetValue(string originalValue)
        {
            var name = originalValue.Substring(originalValue.IndexOf('(') + 1).TrimEnd(')');

            return Context.Current.CurrentPage.RouteData[name];
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SparrowCMS.Core.Labels.Shared.Functions
{
    public class Url : IFunction
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="literalValue">Url(Id)</param>
        /// <returns></returns>
        public string GetParameterValue(string literalValue)
        {
            var name = literalValue.Substring(literalValue.IndexOf('(') + 1).TrimEnd(')');

            return Context.Current.CurrentPage.RouteData[name];
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SparrowCMS.Labels.Shared.Functions
{
    public class Url : ILabelParameterDescriptorFunction
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="literalValue">Url(Id)</param>
        /// <returns></returns>
        public string GetValue(string literalValue)
        {
            var name = literalValue.Substring(literalValue.IndexOf('(') + 1).TrimEnd(')');

            return CMSContext.Current.RouteData[name];
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SparrowCMS.Labels.Shared.Functions
{
    public class Url : IParameterFunction
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="literalValue">Url(Id)</param>
        /// <returns></returns>
        public string GetParameterValue(string literalValue)
        {
            var name = literalValue.Substring(literalValue.IndexOf('(') + 1).TrimEnd(')');

            return CMSContext.Current.RouteData[name];
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using SparrowCMS.Managers;

namespace SparrowCMS.Parsers
{
    public class LabelParameterFunctionParser
    {
        private static readonly Regex ParameterFunctionRegex = new Regex(@"(?<name>[^)(]+)\([^)(]+\)", RegexOptions.Compiled);

        private FactoryManager FactoryManager = FactoryManager.GetInstance();

        public static IParameterFunction Parse(string labelName, string parameterRawValue)
        {
            var match = ParameterFunctionRegex.Match(parameterRawValue);
            if (match == null || match.Groups.Count <= 1) return null;
            var name = match.Groups["name"].Value;
            return FactoryUtils.CreateInstance<IParameterFunction>(labelName, name);
        }

    }
}
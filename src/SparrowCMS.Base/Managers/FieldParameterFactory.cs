using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SparrowCMS.Base.Managers
{
    public class FieldParameterFactory
    {
        private static string[] _namespaces = new string[] {
                "SparrowCMS.Base.Labels.{Label}.Parameters.{Parameter}",
                "SparrowCMS.Base.Labels.Shared.Parameters.{Parameter}",
                "SparrowCMS.Plugin.Labels.{Label}.Parameters.{Parameter}",
                "SparrowCMS.Plugin.Labels.Shared.Parameters.{Parameter}",
            };


        private static IEnumerable<string> GetClassNames(string labelName, string parameterName)
        {
            foreach (var _namespace in _namespaces)
            {
                yield return _namespace.Replace("{Label}", labelName).Replace("{Parameter}", parameterName);
            }
        }

        public static FieldParameter GetInstance(string labelName, string parameterName)
        {
            var type = AssemblyHelper.GetType(GetClassNames(labelName, parameterName).ToArray());
            return AssemblyHelper.CreateInstance<FieldParameter>(type, new FieldParameter());
        }
    }
}

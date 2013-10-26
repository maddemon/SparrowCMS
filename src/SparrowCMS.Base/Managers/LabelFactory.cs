using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SparrowCMS.Base.Managers
{
    public class LabelFactory
    {
        private static string[] _namespaces = new string[]{
                "SparrowCMS.Base.Labels.{Label}.Default"
                ,"SparrowCMS.Plugin.Labels.{Label}.Default"
                ,"SparrowCMS.Base.Labels.{Label}"
                ,"SparrowCMS.Plugin.Labels.{Label}"
        };

        private static IEnumerable<string> GetClassNames(string labelName)
        {
            foreach (var _namespace in _namespaces)
            {
                yield return _namespace.Replace("{Label}", labelName);
            }
        }

        public static LabelBase GetInstance(string labelName)
        {
            var type = AssemblyHelper.GetType(GetClassNames(labelName).ToArray());
            var instance = AssemblyHelper.CreateInstance<LabelBase>(type, null);
            if (instance != null)
            {
                instance.LabelName = labelName;
            }
            return instance;
        }
    }
}

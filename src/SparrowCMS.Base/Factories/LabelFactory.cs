using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SparrowCMS.Base
{
    public class LabelFactory
    {
        private static string[] _namespaces = new string[]{
                "SparrowCMS.Base.Labels.{Label}"
                ,"SparrowCMS.Base.Labels.{Label}.Default"
                ,"SparrowCMS.Plugin.{Plugin}.Labels.{Label}"
                ,"SparrowCMS.Plugin.{Plugin}.Labels.{Label}.Default"
                ,"SparrowCMS.Plugin.{Plugin}.{Label}"
                ,"SparrowCMS.Plugin.{Plugin}.{Label}.Default"
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
            var type = FactoryBase.GetType(GetClassNames(labelName).ToArray());
            var instance = FactoryBase.CreateInstance<LabelBase>(type, null);
            if (instance != null)
            {
                instance.LabelName = labelName;
            }
            return instance;
        }
    }
}

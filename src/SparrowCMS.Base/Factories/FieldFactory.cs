using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SparrowCMS.Base
{
    public class FieldFactory
    {
        private static string[] _namespaces = new string[]{
                "SparrowCMS.Base.Labels.{Label}.Fields.{Field}"
                ,"SparrowCMS.Base.Labels.Shared.Fields.{Field}"
                ,"SparrowCMS.Plugin.{Plugin}.Labels.{Label}.Fields.{Field}"
                ,"SparrowCMS.Plugin.{Plugin}.Labels.Shared.Fields.{Field}"
                ,"SparrowCMS.Plugin.{Plugin}.{Label}.Fields.{Field}"
                ,"SparrowCMS.Plugin.{Plugin}.{Label}.Shared.Fields.{Field}"
        };

        private static IEnumerable<string> GetClassNames(string labelName, string fieldName)
        {
            foreach (var _namespace in _namespaces)
            {
                yield return _namespace.Replace("{Label}", labelName).Replace("{Field}", fieldName);
            }
        }

        public static Field GetInstance(string labelName, string fieldName)
        {
            var type = FactoryBase.GetType(GetClassNames(labelName, fieldName).ToArray());
            return FactoryBase.CreateInstance<Field>(type, new Field());
        }
    }
}

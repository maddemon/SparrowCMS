using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SparrowCMS.Base.Managers
{
    public class CustomFieldManager
    {
        public static IField FindField(string labelName, string fieldName)
        {
            var className = labelName + "." + fieldName;
            return Cache<IField>.GetOrSet(className, () =>
            {
                var type = AssemblyHelper.GetType("Fields", className);
                var field = Activator.CreateInstance(type);
                return field == null ? null : (IField)field;
            });
        }
    }
}

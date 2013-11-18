using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SparrowCMS.Base
{
    public class FieldBuilder
    {
        public static Field Build(FieldDescription description)
        {
            var field = Factory.Instance.GetInstance<Field>(description.LabelName, description.FieldName);
            SetAttributes(field, description);
            field.Name = description.FieldName;
            return field;
        }

        private static void SetAttributes(Field field, FieldDescription description)
        {
            var property = field.GetType().GetProperty("Parameters");
            if (property != null)
            {
                property.SetValue(field, description.Attributes, null);
            }
        }

    }
}

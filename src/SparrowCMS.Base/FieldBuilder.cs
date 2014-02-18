using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SparrowCMS.Core
{
    public class FieldBuilder
    {
        public static Field Build(FieldDescription description)
        {
            var field = Factory.Instance.GetInstance<Field>(description.LabelName, description.FieldName)
                ?? new Field() { TemplateContent = description.TemplateContent };
            SetAttributes(field, description);
            field.Name = description.FieldName;
            field.TemplateContent = description.TemplateContent;
            return field;
        }

        private static void SetAttributes(Field field, FieldDescription description)
        {
            var property = field.GetType().GetProperty("Fields");
            if (property != null)
            {
                property.SetValue(field, description.Attributes, null);
            }
        }

    }
}

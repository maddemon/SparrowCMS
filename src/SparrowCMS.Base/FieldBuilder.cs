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
            field.LabelName = description.LabelName;
            return field;
        }

        private static void SetAttributes(Field field, FieldDescription description)
        {
            foreach (var p in field.GetType().GetProperties())
            {
                var attr = description.Attributes.FirstOrDefault(e => e.Name.ToLower() == p.Name.ToLower());
                p.SetValue(field, attr, null);
            }

            var property = field.GetType().GetProperty("Attributes");
            if (property != null)
            {
                property.SetValue(field, description.Attributes, null);
            }
        }

    }
}

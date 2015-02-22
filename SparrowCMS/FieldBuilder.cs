using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SparrowCMS
{
    public class FieldBuilder
    {
        public static Field Build(FieldDescriptor descriptor)
        {
            var field = FactoryUtils.CreateInstance<Field>(descriptor.LabelName, descriptor.FieldName) ?? new Field() { TemplateContent = descriptor.TemplateContent };

            SetFunctions(field, descriptor);
            field.Name = descriptor.FieldName;
            field.TemplateContent = descriptor.TemplateContent;
            field.LabelName = descriptor.LabelName;
            return field;
        }

        private static void SetFunctions(Field field, FieldDescriptor description)
        {
            foreach (var p in field.GetType().GetProperties())
            {
                var func = description.Functions.FirstOrDefault(e => e.Name.ToLower() == p.Name.ToLower());
                p.SetValue(field, func, null);
            }

            var property = field.GetType().GetProperty("Functions");
            if (property != null)
            {
                property.SetValue(field, description.Functions, null);
            }
        }

    }
}

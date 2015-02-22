using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SparrowCMS.Managers;

namespace SparrowCMS
{
    public class FieldBuilder
    {
        private static FactoryManager FactoryManager = new FactoryManager();

        public static Field Build(FieldDescriptor descriptor)
        {
            Field field = null;
            var factories = FactoryManager.GetFieldFactories();
            foreach (var factory in factories)
            {
                field = factory.CreateField(descriptor);
                if (field != null) break;
            }
            if (field == null)
            {
                field = new Field() { TemplateContent = descriptor.TemplateContent };
            }
            SetFunctions(field, descriptor);
            field.Name = descriptor.FieldName;
            field.TemplateContent = descriptor.TemplateContent;
            field.LabelName = descriptor.LabelDescriptor.LabelName;
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

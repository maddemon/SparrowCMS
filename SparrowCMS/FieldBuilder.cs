using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SparrowCMS
{
    internal class FieldBuilder
    {
        public static IField Build(FieldDescriptor descriptor)
        {
            var field = FactoryUtils.CreateInstance<IField>(descriptor.LabelName, descriptor.FieldName) ?? new DefaultField()
            {
                TemplateContent = descriptor.TemplateContent,
                Name = descriptor.FieldName,
                LabelName = descriptor.LabelName
            };

            SetFunctions(field, descriptor);
            return field;
        }

        private static void SetFunctions(IField field, FieldDescriptor description)
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

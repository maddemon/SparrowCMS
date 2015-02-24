using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SparrowCMS.Managers;

namespace SparrowCMS
{
    internal class FieldBuilder
    {
        public static IField Build(FieldDescriptor descriptor)
        {
            var field = LabelFactory.CreateField(descriptor.LabelName, descriptor.FieldName) ?? new DefaultField()
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
            if (description.Functions.Count() > 0)
            {
                foreach (var p in field.GetType().GetProperties())
                {
                    var func = description.Functions.FirstOrDefault(e => e.Name.ToLower() == p.Name.ToLower());
                    if (func != null)
                    {
                        p.SetValue(field, func, null);
                    }
                }

            }
            var property = field.GetType().GetProperty("Functions");
            if (property != null)
            {
                property.SetValue(field, description.Functions, null);
            }
        }

    }
}

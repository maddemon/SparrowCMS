using SparrowCMS.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SparrowCMS
{
    public class LabelBuilder
    {
        public static ILabel Build(LabelDescriptor labelDescription)
        {
            var label = Factory.Instance.GetInstance<ILabel>(labelDescription.LabelName, labelDescription.ClassName);
            if (label != null)
            {
                //SetInnerLabel(label, labelDescription);
                SetParameters(label, labelDescription);
                SetFields(label, labelDescription);
            }
            return label;
        }

        private static void SetInnerLabel(ILabel label, LabelDescriptor labelDescription)
        {
            if (labelDescription.InnerLabelDescriptors.Count() == 0) return;
            foreach (var p in label.GetType().GetProperties())
            {
                if (p.PropertyType is ILabel)
                {
                    var innerLabelDesc = labelDescription.InnerLabelDescriptors.FirstOrDefault(e => e.LabelName.ToLower() == p.Name.ToLower());

                    p.SetValue(label, Build(innerLabelDesc), null);
                }
            }
        }

        private static void SetParameters(ILabel label, LabelDescriptor labelDescription)
        {
            foreach (var p in label.GetType().GetProperties())
            {
                var attribute = p.GetCustomAttributes(typeof(FieldFlagAttribute), false).FirstOrDefault();
                if (attribute != null) continue;

                if (labelDescription.Parameters.ContainsKey(p.Name.ToLower()))
                {
                    var parameter = labelDescription.Parameters[p.Name.ToLower()];
                    p.SetValue(label, parameter.ConvertParameterValue(p.PropertyType), null);
                }
            }

        }

        private static void SetFields(ILabel label, LabelDescriptor labelDescription)
        {
            foreach (var p in label.GetType().GetProperties())
            {
                var attribute = p.GetCustomAttributes(typeof(FieldFlagAttribute), false).FirstOrDefault();
                if (attribute == null)
                {
                    if (p.Name == "Fields")
                    {
                        var list = new List<Field>();
                        foreach (var fieldDescription in labelDescription.FieldDescriptors)
                        {
                            list.Add(FieldBuilder.Build(fieldDescription));
                        }
                        p.SetValue(label, list, null);
                    }
                }
                else
                {
                    var fieldDesc = labelDescription.FieldDescriptors.FirstOrDefault(e => e.FieldName.ToLower() == p.Name.ToLower());
                    if (fieldDesc != null)
                    {
                        var field = FieldBuilder.Build(fieldDesc);
                        if (field.GetType() != p.PropertyType) continue;
                        p.SetValue(label, field, null);
                    }
                }
            }
        }
    }
}

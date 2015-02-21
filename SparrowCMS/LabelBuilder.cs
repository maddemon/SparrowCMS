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
            //通过LabelFactory获取对应的Label类实例对象.
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

        /// <summary>
        /// 对Label实例的Parameter赋值
        /// </summary>
        /// <param name="label"></param>
        /// <param name="labelDescription"></param>
        private static void SetParameters(ILabel label, LabelDescriptor labelDescription)
        {
            foreach (var p in label.GetType().GetProperties())
            {
                //如果Label类的属性标记了FieldFlag属性,则说明此属性是字段,不是参数,直接continue
                var attribute = p.GetCustomAttributes(typeof(FieldFlagAttribute), false).FirstOrDefault();
                if (attribute != null) continue;

                //如果descriptor的参数里包含了该属性名称,则为label实例的参数
                if (labelDescription.Parameters.ContainsKey(p.Name.ToLower()))
                {
                    var parameter = labelDescription.Parameters[p.Name.ToLower()];
                    p.SetValue(label, parameter.ConvertParameterValue(p.PropertyType), null);
                }
            }

        }

        /// <summary>
        /// 对Label对象的 Fields赋值
        /// </summary>
        /// <param name="label"></param>
        /// <param name="labelDescription"></param>
        private static void SetFields(ILabel label, LabelDescriptor labelDescription)
        {
            foreach (var p in label.GetType().GetProperties())
            {
                //获取该属性是否标记了FieldFlag属性
                var attribute = p.GetCustomAttributes(typeof(FieldFlagAttribute), false).FirstOrDefault();
                if (attribute == null)
                {
                    //如果没有标记,则判断是不是Field属性
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
                    //如果标记,则获取该field的descriptor,并创建对应的field类实例
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

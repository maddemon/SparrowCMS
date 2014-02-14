using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SparrowCMS.Core
{
    public  class LabelBuilder
    {
        public static ILabel Build(LabelDescription labelDescription)
        {
            var label = Factory.Instance.GetInstance<ILabel>(labelDescription.LabelName);
            
            SetParameters(label, labelDescription);
            SetFields(label, labelDescription);

            return label;
        }

        private static void SetParameters(ILabel label, LabelDescription labelDescription)
        {
            foreach (var p in label.GetType().GetProperties())
            {
                if (labelDescription.Parameters.ContainsKey(p.Name.ToLower()))
                {
                    var parameter = labelDescription.Parameters[p.Name.ToLower()];
                    p.SetValue(label, parameter.ConvertParameterValue(), null);
                }
            }

        }

        private static void SetFields(ILabel label, LabelDescription labelDescription)
        {
            var p = label.GetType().GetProperty("Fields");
            if (p != null)
            { 
                var list = new List<Field>();
                foreach(var fieldDescription in labelDescription.FieldDescriptions)
                {
                    list.Add(FieldBuilder.Build(fieldDescription));
                }
                p.SetValue(label, list, null);
            }
        }
    }
}

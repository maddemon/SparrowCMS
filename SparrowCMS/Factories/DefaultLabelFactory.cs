using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SparrowCMS.Managers;

namespace SparrowCMS.Factories
{
    public interface ILabelFactory
    {
        ILabel CreateLabel(LabelDescriptor descriptor);
    }

    public class DefaultLabelFactory : ILabelFactory
    {
        protected static AssemblyManager AssemblyManager = new AssemblyManager();
        
        protected virtual string[] GetFullNames(LabelDescriptor descriptor)
        {
            return new string[]
            { 
                string.Format("{0}.Labels.{1}",descriptor.PluginName , descriptor.ClassName ),
                string.Format("{0}.Shared.Labels.{1}",descriptor.PluginName , descriptor.ClassName ),
                string.Format("SparrowCMS.Labels.{0}",descriptor.LabelName),
                string.Format("SparrowCMS.Labels.{0}.{1}",descriptor.LabelName,descriptor.ClassName),
                string.Format("SparrowCMS.Shared.Labels.{0}",descriptor.LabelName),
                string.Format("SparrowCMS.Shared.Labels.{0}.{1}",descriptor.LabelName,descriptor.ClassName)
            };
        }

        public virtual ILabel CreateLabel(LabelDescriptor descriptor)
        {
            return AssemblyManager.CreateInstance<ILabel>(GetFullNames(descriptor));
        }
    }
}

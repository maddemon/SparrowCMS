using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SparrowCMS.Managers;

namespace SparrowCMS.Factories
{
    public interface IFieldFactory
    {
        Field CreateField(FieldDescriptor descriptor);
    }

    public class DefaultFieldFactory : IFieldFactory
    {
        protected static AssemblyManager AssemblyManager = new AssemblyManager();

        protected virtual string[] GetFullNames(FieldDescriptor descriptor)
        {
            var pluginName = descriptor.LabelDescriptor.PluginName;
            var labelName = descriptor.LabelDescriptor.LabelName;

            return LabelFactoryHelper.GetFullNames(pluginName, labelName, descriptor.FieldName, "Field");
        }

        public virtual Field CreateField(FieldDescriptor descriptor)
        {
            return AssemblyManager.CreateInstance<Field>(GetFullNames(descriptor));

        }
    }
}

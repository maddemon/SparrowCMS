using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SparrowCMS.Factories;

namespace SparrowCMS
{
    internal class LabelFactory
    {
        private static List<ILabelFactory> GetLabelFactories()
        {
            //TODO
            return new List<ILabelFactory>() { new DefaultLabelFactory() };
        }

        private static T CreateInstance<T>(string labelName, string className)
        {
            var factories = GetLabelFactories();
            foreach (var factory in factories)
            {
                var result = factory.CreateInstance<T>(labelName, className);
                if (result != null)
                {
                    return result;
                }
            }
            return default(T);
        }

        public static ILabel CreateLabel(string labelName)
        {
            return CreateInstance<ILabel>(labelName, null);
        }

        public static IField CreateField(string labelName, string fieldName)
        {
            return CreateInstance<IField>(labelName, fieldName);
        }

        public static FieldFunction CreateFieldFunction(string labelName, string functionName)
        {
            return CreateInstance<FieldFunction>(labelName, functionName);
        }

        public static ILabelParameterFunction CreateParameterFunction(string labelName, string functionName)
        {
            return CreateInstance<ILabelParameterFunction>(labelName, functionName);
        }
    }
}

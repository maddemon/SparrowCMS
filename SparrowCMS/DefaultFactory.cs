using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SparrowCMS.Managers;

namespace SparrowCMS
{
    public interface IFactory
    {
        T CreateInstance<T>(string fullName);
    }

    public class DefaultFactory : IFactory
    {
        protected AssemblyManager AssemblyManager;

        public DefaultFactory()
        {
            AssemblyManager = CMSContext.Current.Core.AssemblyManager;
        }

        protected virtual string[] GetFullNames(string fullName)
        {
            var dotIndex = fullName.LastIndexOf('.');
            var parentName = fullName.Substring(0, dotIndex == -1 ? fullName.Length : dotIndex);
            var className = fullName.Substring(dotIndex + 1);

            return new string[]
            {
                string.Format("{0}.{2}s.{1}",parentName , className),
                string.Format("{0}.Shared.{2}s.{1}",parentName , className),
                string.Format("SparrowCMS.{2}s.{0}",parentName),
                string.Format("SparrowCMS.{2}s.{0}.{1}",parentName,className),
                string.Format("SparrowCMS.Shared.{2}s.{0}",parentName),
                string.Format("SparrowCMS.Shared.{2}s.{0}.{1}",parentName,className)
            };
        }

        public T CreateInstance<T>(string[] namespaces)
        {
            foreach (var ns in namespaces)
            {
                var type = AssemblyManager.Search(ns);
                if (type != null)
                {
                    return Activator.CreateInstance<T>();
                }
            }
            return default(T);
        }

        public T CreateInstance<T>(string classFullName)
        {
            var fullNames = GetFullNames(classFullName);
            foreach (var fullName in fullNames)
            {
                var type = AssemblyManager.Search(fullName);
                if (type != null)
                {
                    return Activator.CreateInstance<T>();
                }
            }
            return default(T);
        }
    }
}

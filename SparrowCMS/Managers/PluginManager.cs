using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using SparrowCMS.Models;

namespace SparrowCMS.Managers
{
    public class PluginManager
    {
        public List<Plugin> GetPlugins()
        {
            //TODO
            return new List<Plugin> { new Plugin { EnName = "Article", Name = "文章模块", Id = "1", Version = "1.0", Description = "简单文章发布模块" } };
        }

        private AssemblyManager AssemblyManager = new AssemblyManager();

        public void LoadPulgins()
        {
            foreach (var plugin in GetPlugins())
            {
                AssemblyManager.LoadDll(plugin);
            }
        }
    }
}

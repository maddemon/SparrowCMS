using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SparrowCMS.Managers
{
    public class ManagerCenter
    {
        private ManagerCenter()
        {

        }
        public static readonly ManagerCenter Instance = new ManagerCenter();

        private AssemblyManager _assemblyManager = new AssemblyManager();
        public AssemblyManager AssemblyManager
        {
            get { return _assemblyManager; }
        }

        private PluginManager _pluginManager = new PluginManager();
        public PluginManager PluginManager
        {
            get { return _pluginManager; }
        }

        private SiteManager _siteManager = new SiteManager();
        public SiteManager SiteManager
        {
            get { return _siteManager; }
        }

        private PageManager _pageManager = new PageManager();
        public PageManager PageManager
        {
            get { return _pageManager; }
        }

    }
}

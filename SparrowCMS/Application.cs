using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using SparrowCMS.Common;
using SparrowCMS.Managers;
using SparrowCMS.Models;

namespace SparrowCMS
{
    public class Application
    {
        public static readonly Application Instance = new Application();

        public virtual void Init()
        {
            //加载所有插件
            LoadPlugins();

            //加载所有Factory
            LoadFactories();
        }

        protected virtual void LoadDataProviders()
        {
        }

        protected virtual void LoadPlugins()
        {
            //从plugins目录载入所有插件dll
            new PluginManager().LoadPulgins();
        }

        protected virtual void LoadFactories()
        {

        }

        public void Unload()
        {
        }
    }
}

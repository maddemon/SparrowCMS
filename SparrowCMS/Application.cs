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
        protected readonly CoreManager Core = CoreManager.GetInstance();

        public virtual void Init()
        {
            //加载对应的DataProvider，可以顺利读取数据
            LoadDataProvider();

            //加载所有插件
            LoadPlugins();
        }


        protected virtual void LoadDataProvider()
        {
            var dbType = AppSettings.Current["DbType"];
            AppDomain.CurrentDomain.Load("SparrowCMS.DataProvider." + dbType + ".dll");
        }

        protected virtual void LoadPlugins()
        { 
            //从plugins目录载入所有插件dll
            Core.PluginManager.LoadPulgins();
        }

    }
}

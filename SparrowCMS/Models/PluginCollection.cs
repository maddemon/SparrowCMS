using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SparrowCMS.DataProviders;

namespace SparrowCMS.Models
{
    public class PluginCollection
    {
        private IPluginDataProvider _db;

        public PluginCollection()
        {
            _db = DataProviderFactory.GetProvider<IPluginDataProvider>();
        }

        public List<Plugin> Scan()
        {
            //扫描站点下的plugins的plugin.config文件
            throw new NotImplementedException();
        }

        public Plugin GetPlugin(string enName)
        {
            var list = _db.GetPlugins();
            return list.FirstOrDefault(e => e.EnName.ToLower() == enName.ToLower());
        }
    }
}

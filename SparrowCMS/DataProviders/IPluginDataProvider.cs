using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SparrowCMS.Models;

namespace SparrowCMS.DataProviders
{
    public interface IPluginDataProvider
    {
        List<Plugin> GetPlugins();
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SparrowCMS.Models
{
    public class Plugin
    {
        public string Id { get; set; }

        public string Name { get; set; }

        /// <summary>
        /// 用于目录名
        /// </summary>
        public string EnName { get; set; }

        public string Description { get; set; }

        public string Author { get; set; }

        public string Version { get; set; }

        public PluginStatus Status { get; set; }

        public void LoadDll()
        {
            var pluginPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, EnName);
            var dirs = Directory.GetDirectories(pluginPath);
            if (dirs.Contains("bin"))
            {
                pluginPath = Path.Combine(pluginPath, "bin");
            }
            foreach (var fileName in Directory.GetFiles(pluginPath))
            {
                if (fileName.EndsWith("dll"))
                {
                    var filePath = Path.Combine(pluginPath, fileName);
                    Assembly.LoadFile(filePath);
                }
            }
        }

    }

    public enum PluginStatus
    {
        Enabled,
        Disabled,
        MissFile
    }
}

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace SparrowCMS.Core.Common
{
    public class AppSettings
    {
        public static AppSettings Current = new AppSettings();

        private readonly Dictionary<string, string> _values;

        private AppSettings()
        {
            _values = new Dictionary<string, string>();
            foreach (var key in ConfigurationManager.AppSettings.AllKeys)
            {
                this[key] = ConfigurationManager.AppSettings[key];
            }
        }


        public string this[string path]
        {
            get
            {
                path = path.ToLower();
                return _values.ContainsKey(path) ? _values[path] : string.Empty;
            }
            set
            {
                path = path.ToLower();
                if (_values.ContainsKey(path))
                {
                    _values[path] = value;
                }
                else
                {
                    _values.Add(path, value);
                }
            }
        }
    }
}

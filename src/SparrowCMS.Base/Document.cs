using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;

namespace SparrowCMS.Core
{
    public class Document : DynamicObject
    {
        private Dictionary<string, object> _data = new Dictionary<string, object>();

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            var key = binder.Name.ToLower();
            return _data.TryGetValue(key, out result);
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            this[binder.Name.ToLower()] = value;
            return true;
        }

        public object this[string key]
        {
            get
            {
                return _data.ContainsKey(key) ? _data[key] : null;
            }
            set
            {
                if (_data.ContainsKey(key))
                    _data[key] = value;
                else
                    _data.Add(key, value);
            }
        }
    }

}

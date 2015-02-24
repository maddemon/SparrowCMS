using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using Newtonsoft.Json.Linq;

namespace SparrowCMS.ModelBinders
{
    public class JsonBinder : IModelBinder
    {
        private const string CacheKey = "JsonFieldModel";
        private JObject _data
        {
            get
            {
                return HttpContext.Current.Items[CacheKey] as JObject;
            }
            set
            {
                if (value == null) return;
                HttpContext.Current.Items[CacheKey] = value;
            }
        }

        public object BindModel(ApiActionParameterDescriptor descriptor)
        {
            var request = CMSContext.Current.HttpContext.Request;
            if (_data == null)
            {
                using (var sr = new StreamReader(request.InputStream))
                {
                    var json = sr.ReadToEnd();
                    _data = Newtonsoft.Json.JsonConvert.DeserializeObject<JObject>(json);
                }
            }

            if (_data == null)
            {
                return null;
            }

            var obj = _data[descriptor.ParameterName];

            return obj == null ? null : obj.ToObject(descriptor.ParameterType);
        }
    }
}

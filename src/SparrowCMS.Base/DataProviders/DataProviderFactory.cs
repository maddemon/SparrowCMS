using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SparrowCMS.Core.DataProviders
{
    public class DataProviderFactory
    {
        public static T GetProvider<T>()
        {
            throw new NotImplementedException();
            //load dal.dll
            //create instance
            //return
        }
    }
}

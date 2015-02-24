using SparrowCMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SparrowCMS
{
    public interface IApi
    { 
        
    }

    public class ApiBase : IApi
    {
        public virtual void OnError() { }

        protected virtual APIResult Success(object data = null, string message = null)
        {
            return new APIResult { Data = data, Message = message, Result = true };
        }

        protected virtual APIResult Error(string message = null)
        {
            return new APIResult { Result = false, Data = null, Message = message };
        }
    }
}

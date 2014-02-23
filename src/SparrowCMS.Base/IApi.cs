using SparrowCMS.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SparrowCMS.Core
{
    public interface IApi
    { 
        
    }

    public class APIBase : IApi
    {
        public virtual void OnError() { }

        protected virtual ApiResult Success(object data = null, string message = null)
        {
            return new ApiResult { Data = data, Message = message, Result = true };
        }

        protected virtual ApiResult Error(string message = null)
        {
            return new ApiResult { Result = false, Data = null, Message = message };
        }
    }
}

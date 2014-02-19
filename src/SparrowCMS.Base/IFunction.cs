using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SparrowCMS.Core
{
    public interface IFunction
    {
        string GetParameterValue(string literalValue);
    }
}

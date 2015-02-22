using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SparrowCMS
{
    public interface IParameterFunction
    {
        string GetParameterValue(string literalValue);
    }
}

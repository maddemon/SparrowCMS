using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SparrowCMS
{
    public interface ILabelParameterFunction
    {
        string GetParameterValue(string literalValue);
    }
}

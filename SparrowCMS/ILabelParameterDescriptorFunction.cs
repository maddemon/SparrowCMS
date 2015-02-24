using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SparrowCMS
{
    public interface ILabelParameterDescriptorFunction
    {
        string GetValue(string parameterRawValue);
    }
}

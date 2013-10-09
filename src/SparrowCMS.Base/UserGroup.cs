using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SparrowCMS.Framework
{
    [Flags]
    public enum UserGroup
    {
        Everyone = 1,
        Users = 2,
        Administrators = 4,
    }
}

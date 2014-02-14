using System;

namespace SparrowCMS.Core
{
    [Flags]
    public enum UserGroup
    {
        Everyone = 1,
        Users = 2,
        Administrators = 4,
    }
}

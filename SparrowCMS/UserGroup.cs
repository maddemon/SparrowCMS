using System;

namespace SparrowCMS
{
    [Flags]
    public enum UserGroup
    {
        Everyone = 1,
        Users = 2,
        Administrators = 4,
    }
}

using System;

namespace SparrowCMS.Base
{
    [Flags]
    public enum UserGroup
    {
        Everyone = 1,
        Users = 2,
        Administrators = 4,
    }
}

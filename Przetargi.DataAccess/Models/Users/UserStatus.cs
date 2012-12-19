using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Przetargi.DataAccess.Models.Users
{
    public enum UserStatus
    {
        NewInactive = 1,
        Active = 2,
        Inactive = 3,
        Blocked = 4,
    }
}

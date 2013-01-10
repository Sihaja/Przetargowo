using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Przetargi.DataAccess.Models.Users
{
    [Serializable]
    public class TenderAttendeeUser : User
    {
        public override UserType Type { get { return UserType.TenderAttendee; } }
    }
}

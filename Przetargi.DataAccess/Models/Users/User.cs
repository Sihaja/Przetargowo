using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Przetargi.DataAccess.Models.Users
{
    public abstract class User
    {
        public int Id { get; set; }

        public abstract UserType Type { get; }

        public string Name { get; set; }

        public string Email { get; set; }

        public UserStatus Status { get; set; }
    }
}

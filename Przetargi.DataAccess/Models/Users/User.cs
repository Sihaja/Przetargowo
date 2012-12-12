using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Przetargi.DataAccess.Models.Users
{
    public abstract class User
    {
        public int Id { get; set; }

        public UserType Type { get; set; }
    }
}

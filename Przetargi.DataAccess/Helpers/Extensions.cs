using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Przetargi.DataAccess.Helpers
{
    public static class Extensions
    {
        public static object NullIfDbNull(this object o)
        {
            return o is DBNull ? null : o;
        }
    }
}

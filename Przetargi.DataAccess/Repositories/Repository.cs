using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Przetargi.DataAccess.Repositories
{
    public static class Repository
    {
        private static Lazy<IRepository> _instance = new Lazy<IRepository>(() => 
                                    new SqlRepository(ConfigurationManager.ConnectionStrings["PrzetargiDb"].ConnectionString), false);

        public static IRepository Instance
        {
            get { return _instance.Value; }
        }
    }
}

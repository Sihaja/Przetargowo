using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Przetargi.DataAccess.Models;
using System.Data.SqlClient;
using System.Data;

namespace Przetargi.DataAccess.Repositories
{
    public partial class SqlRepository : IRepository
    {
        private string _connectionString;

        public SqlRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
    }
}

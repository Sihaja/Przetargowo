using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Przetargi.DataAccess.Models.Users;
using System.Data.SqlClient;

namespace Przetargi.DataAccess.Repositories
{
    public partial class SqlRepository
    {
        public User CreateUser(User user)
        {
            if (user.Type == UserType.TenderOwner)
                return CreateTenderOwner(user as TenderOwnerUser);
            else if (user.Type == UserType.TenderAttendee)
                return CreateTenderAttendee(user as TenderAttendeeUser);
            else if (user.Type == UserType.Administrator)
                throw new NotImplementedException();
            else
                throw new ArgumentException("Invalid user type");
        }

        private User CreateTenderOwner(TenderOwnerUser user)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                using (SqlCommand comm = conn.CreateCommand())
                {
                    throw new NotImplementedException();
                }
            }
        }

        private User CreateTenderAttendee(TenderAttendeeUser user)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                using (SqlCommand comm = conn.CreateCommand())
                {
                    throw new NotImplementedException();
                }
            }
        }

        public User AuthenticateUser(string userName, string password)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                using (SqlCommand comm = conn.CreateCommand())
                {
                    throw new NotImplementedException();
                }
            }
        }
    }
}

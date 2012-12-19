﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Net.Mail;
using Przetargi.DataAccess.Models.Users;
using Przetargi.DataAccess.Helpers;
using Przetargi.ExternalAccess.Email;

namespace Przetargi.DataAccess.Repositories
{
    public partial class SqlRepository
    {
        public User CreateUser(User user, string password)
        {
            if (user.Type == UserType.TenderOwner)
                return CreateTenderOwner(user as TenderOwnerUser, password);
            else if (user.Type == UserType.TenderAttendee)
                return CreateTenderAttendee(user as TenderAttendeeUser, password);
            else if (user.Type == UserType.Administrator)
                throw new NotImplementedException();
            else
                throw new ArgumentException("Invalid user type");
        }

        private User CreateTenderOwner(TenderOwnerUser user, string password)
        {
            Guid token = Guid.NewGuid();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                using (SqlCommand comm = conn.CreateCommand())
                {
                    comm.CommandType = System.Data.CommandType.StoredProcedure;
                    comm.CommandText = "[CreateUser]";

                    comm.Parameters.AddRange(new[]
                    {
                        new SqlParameter("@type", (int)user.Type),
                        new SqlParameter("@name", user.Name),
                        new SqlParameter("@password", password),
                        new SqlParameter("@email", user.Email),
                        new SqlParameter("@activationToken", token),
                    });

                    comm.ExecuteNonQuery();
                }
            }

            IMailer mailer = new Mailer();
            mailer.SendMail(new[] { user.Email }, "Potwierdzenie rejestracji", token.ToString());

            return AuthenticateUser(user.Name, password, user.Type);
        }

        private User CreateTenderAttendee(TenderAttendeeUser user, string password)
        {
            Guid token = Guid.NewGuid();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                using (SqlCommand comm = conn.CreateCommand())
                {
                    comm.CommandType = System.Data.CommandType.StoredProcedure;
                    comm.CommandText = "[CreateUser]";

                    comm.Parameters.AddRange(new[]
                    {
                        new SqlParameter("@type", (int)user.Type),
                        new SqlParameter("@name", user.Name),
                        new SqlParameter("@password", password),
                        new SqlParameter("@email", user.Email),
                        new SqlParameter("@activationToken", token),
                    });

                    comm.ExecuteNonQuery();
                }
            }

            IMailer mailer = new Mailer();
            mailer.SendMail(new [] { user.Email }, "Potwierdzenie rejestracji", token.ToString());

            return AuthenticateUser(user.Name, password, user.Type);
        }

        public User AuthenticateUser(string userName, string password, UserType type)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                using (SqlCommand comm = conn.CreateCommand())
                {
                    comm.CommandType = System.Data.CommandType.StoredProcedure;
                    comm.CommandText = "[AuthenticateUser]";

                    comm.Parameters.AddRange(new[]
                    {
                        new SqlParameter("@name", userName),
                        new SqlParameter("@password", password),
                        new SqlParameter("@type", (int)type),
                    });

                    using (SqlDataReader dr = comm.ExecuteReader())
                    {
                        dr.Read();

                        return new TenderAttendeeUser
                        {
                            Id = (int)dr["Id"],
                            Email = (string)dr["Email"].NullIfDbNull(),
                            Name = (string)dr["Name"],
                            Status = (UserStatus)dr["Status"],
                        };
                    }
                }
            }
        }
    }
}

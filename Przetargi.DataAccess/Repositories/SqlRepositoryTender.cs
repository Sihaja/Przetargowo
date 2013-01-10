using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Przetargi.DataAccess.Models.Tenders;
using System.Data.SqlClient;
using System.Data;

namespace Przetargi.DataAccess.Repositories
{
    public partial class SqlRepository : IRepository
    {
        public List<TenderOffer> GetOffersForTender(int? tenderId = null, int? attendeeId = null, DateTime? postedFrom = null, DateTime? postedTo = null, int? top = 10, int? page = 0)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                using (SqlCommand comm = conn.CreateCommand())
                {
                    comm.CommandType = System.Data.CommandType.StoredProcedure;
                    comm.CommandText = "[GetTenderOffers]";

                    comm.Parameters.AddRange(new[]
                    {
                        new SqlParameter("@tenderId", tenderId),
                        new SqlParameter("@attendeeId", attendeeId),
                        new SqlParameter("@postedDateFrom", postedFrom),
                        new SqlParameter("@postedDateTo", postedTo),
                        new SqlParameter("@top", top),
                        new SqlParameter("@page", page),
                    });

                    using (SqlDataReader dr = comm.ExecuteReader())
                    {
                        return (from IDataRecord row
                                in dr.Cast<IDataRecord>()
                                select new TenderOffer
                                {
                                    TenderId = (int)row["TenderId"],
                                    AttendeeId = (int)row["AttendeeId"],
                                    Price = (decimal)row["Price"],
                                    PostedDate = (DateTime)row["PostedDate"],
                                }).ToList();
                    }
                }
            }
        }

        public List<Tender> GetTenders(int? tenderId = null, string tenderName = null, DateTime? from = null, DateTime? to = null, int? byOwnerId = null, int? top = 10, int? page = 0)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                using (SqlCommand comm = conn.CreateCommand())
                {
                    comm.CommandType = System.Data.CommandType.StoredProcedure;
                    comm.CommandText = "[GetTenders]";

                    comm.Parameters.AddRange(new[]
                    {
                        new SqlParameter("@tenderId", tenderId),
                        new SqlParameter("@tenderName", tenderName),
                        new SqlParameter("@from", from),
                        new SqlParameter("@to", to),
                        new SqlParameter("@ownerId", byOwnerId),
                        new SqlParameter("@top", top),
                        new SqlParameter("@page", page)
                    });

                    using (SqlDataReader dr = comm.ExecuteReader())
                    {
                        return (from IDataRecord row
                                in dr.Cast<IDataRecord>()
                                select new Tender
                                {
                                    Id = (int)row["Id"],
                                    OwnerId = (int)row["OwnerId"],
                                    PostedDate = (DateTime)row["PostedDate"],
                                    EndingDate = (DateTime)row["EndingDate"],
                                    ProjectName = (string)row["ProjectName"],
                                    ProjectBudget = (decimal)row["ProjectBudget"],
                                    Status = (TenderStatus)row["Status"],
                                    ContactPersonFirstName = (string)row["ContactPersonFirstName"],
                                    ContactPersonLastName = (string)row["ContactPersonLastName"],
                                    ContactPersonTelNo = (string)row["ContactPersonTelNo"],
                                    ContactPersonEmail = (string)row["ContactPersonEmail"]
                                }).ToList();
                    }
                }
            }
        }

        public void AddTenderOffer(TenderOffer tenderOffer)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                using (SqlCommand comm = conn.CreateCommand())
                {
                    comm.CommandType = System.Data.CommandType.StoredProcedure;
                    comm.CommandText = "[AddTenderOffer]";

                    comm.Parameters.AddRange(new[]
                    {
                        new SqlParameter("@tenderId", tenderOffer.TenderId),
                        new SqlParameter("@attendeeId", tenderOffer.AttendeeId),
                        new SqlParameter("@price", tenderOffer.Price),
                    });

                    comm.ExecuteNonQuery();
                }
            }
        }

        public int AddTender(Tender tender)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                using (SqlCommand comm = conn.CreateCommand())
                {
                    comm.CommandType = System.Data.CommandType.StoredProcedure;
                    comm.CommandText = "[CreateTender]";

                    comm.Parameters.AddRange(new[]
                    {
                        new SqlParameter("@ownerId", tender.OwnerId),
                        new SqlParameter("@projectName", tender.ProjectName),
                        new SqlParameter("@projectBudget", tender.ProjectBudget),
                        new SqlParameter("@endingDate", tender.EndingDate),
                        new SqlParameter("@contactPersonFirstName", tender.ContactPersonFirstName),
                        new SqlParameter("@contactPersonLastName", tender.ContactPersonLastName),
                        new SqlParameter("@contactPersonTelNo", tender.ContactPersonTelNo),
                        new SqlParameter("@contactPersonEmail", tender.ContactPersonEmail)
                    });

                    return Convert.ToInt32(comm.ExecuteScalar());
                }
            }
        }
    }
}

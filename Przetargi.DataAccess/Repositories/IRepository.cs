using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Przetargi.DataAccess.Models.Users;
using Przetargi.DataAccess.Models.Tenders;

namespace Przetargi.DataAccess.Repositories
{
    public interface IRepository
    {
        /*** Users ***/
        User CreateUser(User user, string password);
        User GetUser(string name);
        User AuthenticateUser(string userName, string password, UserType type);

        /*** Tenders ***/
        List<TenderOffer> GetOffersForTender(int? tenderId = null, int? attendeeId = null, DateTime? postedFrom = null, DateTime? postedTo = null, int? top = 10, int? page = 0);
        List<Tender> GetTenders(int? tenderId = null, string tenderName = null, DateTime? from = null, DateTime? to = null, int? byOwnerId = null, int? top = null, int? page = null);
        int AddTender(Tender tender);
        void AddTenderOffer(TenderOffer tenderOffer);
    }
}

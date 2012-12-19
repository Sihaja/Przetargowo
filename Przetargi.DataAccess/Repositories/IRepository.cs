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
        User AuthenticateUser(string userName, string password, UserType type);

        /*** Tenders ***/
        List<TenderOffer> GetOffersForTender(int tenderId);
        List<Tender> GetTenders(int? tenderId, string tenderName, DateTime? from, DateTime? to, int? byOwnerId, int? top);
        void AddTenderOffer(Tender tender, TenderOffer tenderOffer);
    }
}

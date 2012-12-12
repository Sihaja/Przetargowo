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
        User CreateUser(User user);
        User AuthenticateUser(string userName, string password);

        /*** Tenders ***/
        List<TenderOffer> GetOffersForTender(int tenderId);
        List<Tender> GetTenders(int? tenderId, string tenderName, DateTime? from, DateTime? to, int? byOwnerId, int? top);
        void AddTenderOffer(Tender tender, TenderOffer tenderOffer);
    }
}

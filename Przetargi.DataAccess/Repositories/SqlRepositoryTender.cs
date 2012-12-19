using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Przetargi.DataAccess.Models.Tenders;

namespace Przetargi.DataAccess.Repositories
{
    public partial class SqlRepository : IRepository
    {
        public List<TenderOffer> GetOffersForTender(int tenderId)
        {
            throw new NotImplementedException();
        }

        public List<Tender> GetTenders(int? tenderId, string tenderName, DateTime? from, DateTime? to, int? byOwnerId, int? top)
        {
            return new List<Tender>();

            throw new NotImplementedException();
        }

        public void AddTenderOffer(Tender tender, TenderOffer tenderOffer)
        {
            throw new NotImplementedException();
        }
    }
}

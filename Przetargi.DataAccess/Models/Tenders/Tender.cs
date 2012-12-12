using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Przetargi.DataAccess.Repositories;

namespace Przetargi.DataAccess.Models.Tenders
{
    public class Tender
    {
        public int Id { get; set; }

        public int OwnerId { get; set; }

        public DateTime PostedDate { get; set; }

        public DateTime EndingDate { get; set; }

        private List<TenderOffer> _offers;
        public List<TenderOffer> Offers
        {
            get 
            {
                if (_offers == null)
                {
                    _offers = Repository.Instance.GetOffersForTender(Id);
                }
                return _offers;
            }
        }

        public void AddOffer(TenderOffer offer)
        {
            Repository.Instance.AddTenderOffer(this, offer);
            _offers = null;
        }
    }
}

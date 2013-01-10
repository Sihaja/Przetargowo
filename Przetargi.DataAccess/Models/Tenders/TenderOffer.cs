using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Przetargi.DataAccess.Models.Tenders
{
    public class TenderOffer
    {
        public int TenderId { get; set; }

        public int AttendeeId { get; set; }

        public decimal Price { get; set; }

        public DateTime PostedDate { get; set; }
    }
}

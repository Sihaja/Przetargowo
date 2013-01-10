using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Przetargi.DataAccess.Models.Tenders;

namespace Przetargi.Models
{
    public class TenderViewModel
    {
        public Tender Tender { get; set; }

        public bool Attendee { get; set; }
    }
}
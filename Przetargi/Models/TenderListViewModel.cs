using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Przetargi.DataAccess.Models.Tenders;

namespace Przetargi.Models
{
    public class TenderListViewModel
    {
        public List<Tender> Tenders { get; set; }

        public string Search { get; set; }
    }
}
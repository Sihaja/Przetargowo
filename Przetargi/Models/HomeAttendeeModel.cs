using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Przetargi.DataAccess.Models.Tenders;

namespace Przetargi.Models
{
    public class HomeAttendeeModel : HomeModel
    {
        public List<Tender> InterestingTenders { get; set; }
    }
}
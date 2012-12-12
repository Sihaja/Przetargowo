using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Przetargi.DataAccess.Models.Tenders;

namespace Przetargi.Models
{
    public class HomeOwnerModel : HomeModel
    {
        public List<Tender> OwnTenders { get; set; }
    }
}
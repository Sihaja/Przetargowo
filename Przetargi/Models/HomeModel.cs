using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Przetargi.DataAccess.Models.Tenders;

namespace Przetargi.Models
{
    public class HomeModel
    {
        public List<Tender> FrontPageTenders { get; set; }
    }
}
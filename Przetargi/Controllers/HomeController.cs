using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Przetargi.Controllers.Extension;
using Przetargi.DataAccess.Repositories;
using Przetargi.Models;
using Przetargi.DataAccess.Models.Tenders;

namespace Przetargi.Controllers
{
    public class HomeController : PrzetargiAbstractController
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to kick-start your ASP.NET MVC application.";

            if (IsOwner)
                return IndexOwner();

            if (IsAttendee)
                return IndexAttendee();

            var tenders = Repository.Instance.GetTenders(null, null, DateTime.Now, null, null, 5, null);

            return View("Index", new HomeModel
            {
                FrontPageTenders = tenders
            });
        }

        private ActionResult IndexOwner()
        {
            var tenders = Repository.Instance.GetTenders(null, null, DateTime.Now, null, null, 5, null);
            var myTenders = Repository.Instance.GetTenders(null, null, null, null, CurrentUser.Id, 5, null);

            return View("Index", new HomeOwnerModel
            {
                FrontPageTenders = tenders,
                OwnTenders = myTenders
            });
        }

        private ActionResult IndexAttendee()
        {
            var tenders = Repository.Instance.GetTenders(null, null, DateTime.Now, null, null, 5, null);
            var interestingTenders = new List<Tender>();

            return View("Index", new HomeAttendeeModel
            {
                FrontPageTenders = tenders,
                InterestingTenders = interestingTenders
            });
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Przetargi.Helpers;
using Przetargi.DataAccess.Models.Users;
using Przetargi.Models;
using Przetargi.Controllers.Extension;
using Przetargi.DataAccess.Models.Tenders;
using Przetargi.DataAccess.Repositories;
using System.IO;

namespace Przetargi.Controllers
{
    public class TenderController : PrzetargiAbstractController
    {
        [AuthorizedOnly()]
        public ActionResult Index()
        {
            List<Tender> tenders = Repository.Instance.GetTenders(null, null, null, null, null, 10, null);

            return View("Index", new TenderListViewModel
            {
                Tenders = tenders,
            });
        }

        [HttpPost]
        [AuthorizedOnly()]
        public ActionResult Search(string searchText, int? pageIndex)
        {
            List<Tender> tenders = Repository.Instance.GetTenders(null, searchText, null, null, null, 10, (pageIndex ?? 0));

            return View("Index", new TenderListViewModel
            {
                Tenders = tenders,
                Search = searchText
            });
        }

        //
        // GET: /Tender/Details/5

        [AuthorizedOnly()]
        public ActionResult Details(int id)
        {
            Tender tender = Repository.Instance.GetTenders(id).FirstOrDefault();

            if (tender == null)
                RedirectToAction("NotFound");

            return View(new TenderViewModel
            {
                Tender = tender,
                Attendee = CurrentUser.Type == UserType.TenderAttendee
            });
        }

        [HttpGet]
        [AuthorizedOnly(Type = UserType.TenderAttendee)]
        public ActionResult Participate(int id)
        {
            var tender = Repository.Instance.GetTenders(id).FirstOrDefault();

            if (tender == null)
                return View(new ParticipateViewModel { Result = ParticipateResult.FailureTenderDoesNotExist });

            if (tender.Offers.Any(o => o.AttendeeId == CurrentUser.Id))
                return View(new ParticipateViewModel { Result = ParticipateResult.FailureCantParticipateTwice });

            return View(new ParticipateViewModel
            {
                AttendeeId = CurrentUser.Id,
                Tender = tender,
            });
        }

        [HttpPost]
        [AuthorizedOnly(Type = UserType.TenderAttendee)]
        public ActionResult Participate(ParticipateViewModel model, HttpPostedFileBase file)
        {
            if (file == null || file.ContentLength == 0)
            {
                model.Result = ParticipateResult.FailureNoDocument;
                return View(model);
            }

            if (model.Tender == null)
                return View(new ParticipateViewModel { Result = ParticipateResult.FailureTenderDoesNotExist });

            if (model.Tender.Offers.Any(o => o.AttendeeId == model.AttendeeId))
                return View(new ParticipateViewModel { Result = ParticipateResult.FailureCantParticipateTwice });

            try
            {
                var fileName = Path.GetFileName(file.FileName);
                var path = String.Format("Tenders/{0}/{1}/{2}", model.Tender.Id, model.AttendeeId, fileName);
                var baseDir = "D:/Przetargi/";
                EnsurePath(baseDir, path);

                using (StreamReader sr = new StreamReader(file.InputStream))
                {
                    System.IO.File.WriteAllText(baseDir + path, sr.ReadToEnd());
                }

                Repository.Instance.AddTenderOffer(new TenderOffer
                {
                    AttendeeId = model.AttendeeId,
                    TenderId = model.Tender.Id,
                    Price = model.Price
                });

                model.Result = ParticipateResult.Success;
            }
            catch (Exception /*exc*/)
            {
                model.Result = ParticipateResult.FailureInternalError;
            }

            return View(model);
        }

        //
        // GET: /Tender/Create

        [AuthorizedOnly(Type = UserType.TenderOwner)]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [AuthorizedOnly(Type = UserType.TenderOwner)]
        public ActionResult Create(TenderViewModel model)
        {
            try
            {
                model.Tender.OwnerId = CurrentUser.Id;
                model.Tender.Status = TenderStatus.New;
                model.Tender.PostedDate = DateTime.Now;

                int tenderId = Repository.Instance.AddTender(model.Tender);

                return RedirectToAction("View", new { id = tenderId });
            }
            catch
            {
                return View();
            }
        }

        [AuthorizedOnly]
        public ActionResult NotFound()
        {
            return View();
        }

        private void EnsurePath(string basePath, string path)
        {
            var split = path.Split(new[] { '/' });
            split = split.Take(split.Count() - 1).ToArray();

            var acc = String.Empty;
            foreach (var s in split)
            {
                acc += s + "/";
                if (!Directory.Exists(basePath + acc))
                    Directory.CreateDirectory(basePath + acc);
            }
        }
    }
}

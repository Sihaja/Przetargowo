using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Przetargi.DataAccess.Models.Users;
using Przetargi.DataAccess.Repositories;
using System.Configuration;

namespace Przetargi.Controllers.Extension
{
    public class PrzetargiAbstractController : Controller
    {
        public User CurrentUser
        {
            get { return Session["CurrentUser"] as User; }
            set { Session["CurrentUser"] = value; }
        }

        public bool IsLoggedIn
        {
            get { return CurrentUser != null; }
        }

        public bool IsOwner
        {
            get { return IsLoggedIn && CurrentUser.Type == UserType.TenderOwner; }
        }

        public bool IsAttendee
        {
            get { return IsLoggedIn && CurrentUser.Type == UserType.TenderAttendee; }
        }
    }
}
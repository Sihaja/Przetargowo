using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Przetargi.DataAccess.Models.Users;
using Przetargi.DataAccess.Repositories;
using System.Configuration;
using System.Web.Security;
using Przetargi.DataAccess.Helpers;

namespace Przetargi.Controllers.Extension
{
    public class PrzetargiAbstractController : Controller
    {
        private static readonly string CookieNameOwner = "przetargilgn";

        private User _currentUser;
        public User CurrentUser
        {
            get { return Session["CurrentUser"] as User ?? Load(); }
            private set { Session["CurrentUser"] = value; }
        }

        private User Load()
        {
            if (HttpContext.User == null || String.IsNullOrEmpty(HttpContext.User.Identity.Name))
                return null;

            return Repository.Instance.GetUser(HttpContext.User.Identity.Name);
        }

        public void OnLogin(User user, bool isPersistent)
        {
            CurrentUser = user;

            FormsAuthentication.SetAuthCookie(user.Name, isPersistent);

            UserType type = user.Type;

            var ticket = new FormsAuthenticationTicket(1, user.Name, DateTime.Now, DateTime.Now.AddMilliseconds(FormsAuthentication.Timeout.TotalMilliseconds), false, ((int)type).ToString());
            var encryptedTicket = FormsAuthentication.Encrypt(ticket);
            var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket)
            {
                Domain = FormsAuthentication.CookieDomain,
                HttpOnly = true,
                Secure = FormsAuthentication.RequireSSL,
            };

            Response.AppendCookie(authCookie);
        }

        public void OnLogout()
        {
            CurrentUser = null;
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
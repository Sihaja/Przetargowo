using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Security.Principal;
using Przetargi.DataAccess.Models.Users;

namespace Przetargi.Helpers
{
    public class AuthorizedOnlyAttribute : AuthorizeAttribute
    {
        public UserType Type { get; set; }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext.User.Identity.IsAuthenticated)
            {
                if ((int)Type == 0)
                    return true;

                var authCookie = httpContext.Request.Cookies[FormsAuthentication.FormsCookieName];
                if (authCookie != null)
                {
                    var ticket = FormsAuthentication.Decrypt(authCookie.Value);
                    int role = Int32.Parse(ticket.UserData ?? "0");
                    return (int)Type == role;
                }
            }

            return base.AuthorizeCore(httpContext);
        }
    }
}
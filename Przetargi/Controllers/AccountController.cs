﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Przetargi.Models;
using Przetargi.DataAccess.Repositories;
using Przetargi.Controllers.Extension;
using Przetargi.DataAccess.Models.Users;

namespace Przetargi.Controllers
{
    [Authorize]
    public class AccountController : PrzetargiAbstractController
    {
        //
        // GET: /Account/Login

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [AllowAnonymous]
        public ActionResult LoginOwner(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [AllowAnonymous]
        public ActionResult LoginAttendee(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login

        [AllowAnonymous]
        [HttpPost]
        public ActionResult LoginAttendee(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var authenticatedUser = Repository.Instance.AuthenticateUser(model.UserName, model.Password, UserType.TenderAttendee);
                if(authenticatedUser != null)
                {
                    OnLogin(authenticatedUser, model.RememberMe);

                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "The user name or password provided is incorrect.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult LoginOwner(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                //if (Membership.ValidateUser(model.UserName, model.Password))
                var authenticatedUser = Repository.Instance.AuthenticateUser(model.UserName, model.Password, UserType.TenderOwner);
                if (authenticatedUser != null)
                {
                    OnLogin(authenticatedUser, model.RememberMe);

                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "The user name or password provided is incorrect.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/LogOff

        public ActionResult LogOff()
        {
            OnLogout();
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult RegisterOwner()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult RegisterOwner(RegisterOwnerModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.User.Status = UserStatus.NewInactive;

                    OnLogin(Repository.Instance.CreateUser(model.User, model.Password), false);

                    return RedirectToAction("Index", "Home");
                }
                catch
                {
                    ModelState.AddModelError("", "The service is unavailable or an error has occured.");
                }
            }

            return View(model);
        }

        [AllowAnonymous]
        public ActionResult RegisterAttendee()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult RegisterAttendee(RegisterAttendeeModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.User.Status = UserStatus.NewInactive;

                    OnLogin(Repository.Instance.CreateUser(model.User, model.Password), false);

                    return RedirectToAction("Index", "Home");
                }
                catch
                {
                    ModelState.AddModelError("", "The service is unavailable or an error has occured.");
                }
            }

            return View(model);
        }

        //
        // GET: /Account/ChangePassword

        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Account/ChangePassword

        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            //if (ModelState.IsValid)
            //{

            //    // ChangePassword will throw an exception rather
            //    // than return false in certain failure scenarios.
            //    bool changePasswordSucceeded;
            //    try
            //    {
            //        MembershipUser currentUser = Membership.GetUser(User.Identity.Name, userIsOnline: true);
            //        changePasswordSucceeded = currentUser.ChangePassword(model.OldPassword, model.NewPassword);
            //    }
            //    catch (Exception)
            //    {
            //        changePasswordSucceeded = false;
            //    }

            //    if (changePasswordSucceeded)
            //    {
            //        return RedirectToAction("ChangePasswordSuccess");
            //    }
            //    else
            //    {
            //        ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
            //    }
            //}

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ChangePasswordSuccess

        public ActionResult ChangePasswordSuccess()
        {
            return View();
        }

        private IEnumerable<string> GetErrorsFromModelState()
        {
            return ModelState.SelectMany(x => x.Value.Errors.Select(error => error.ErrorMessage));
        }

        #region Status Codes
        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
        #endregion
    }
}

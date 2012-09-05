using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Ziggurat.Contracts.Registration;
using Ziggurat.Infrastructure;
using Ziggurat.Registration.Client.Login;
using Ziggurat.Registration.Client.RegistrationStatus;
using Ziggurat.Registration.Web.Models;

namespace Ziggurat.Registration.Web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid && IsAuthenticated(model.UserName, model.Password))
            {
                FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                return RedirectToLocal(returnUrl);
            }

            ModelState.AddModelError("", "The user name or password provided is incorrect.");
            return View(model);
        }

        private bool IsAuthenticated(string login, string password)
        {
            PasswordIndex index;
            if (!Client.ViewModelReader.TryGet<byte, PasswordIndex>(Partition.GetPartition(login), out index))
                return false;

            string realPassword;
            if (!index.Passwords.TryGetValue(login, out realPassword))
                return false;

            return realPassword == password;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        public ActionResult Manage()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Manage(LocalPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                var login = User.Identity.Name;
                PasswordIndex index;
                if (!Client.ViewModelReader.TryGet<byte, PasswordIndex>(Partition.GetPartition(login), out index))
                {
                    ModelState.AddModelError("unknown", "Cannot verify, please try again.");
                    return View();
                }

                string realPassword;
                if (!index.Passwords.TryGetValue(login, out realPassword))
                {
                    ModelState.AddModelError("unknown", "Cannot verify, please try again.");
                    return View();
                }

                if (model.OldPassword != realPassword)
                {
                    ModelState.AddModelError("invalid", "Invalid password");
                    return View();
                }
            }

            return View();
        }

        [AllowAnonymous, HttpPost, ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var regId = RegistrationIdGenerator.NewRegistrationId();

                var data = new RegistrationData(model.UserName, model.Email, model.DisplayName, model.Password, Now.UtcTime);
                var cmd = new StartRegistration(regId, data);

                Client.CommandSender.SendCommand(cmd);

                return View("RegistrationThankYou", regId);
            }
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult RegistrationStatus(Guid id)
        {
            RegistrationStatusView view;

            if (!Client.ViewModelReader.TryGet<Guid, RegistrationStatusView>(id, out view)) 
            {
                view = new RegistrationStatusView { Status = RegistrationProcessStatus.InProgress };
            }

            var viewName = "Registration" + view.Status.ToString();

            return View(viewName, view);
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}

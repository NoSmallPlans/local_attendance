﻿using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using _5051.Models;
using System.Web.Routing;

namespace _5051.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public AccountController()
        {
        }
        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(CredentialsViewModel model, string returnUrl)
        {
            if (model.Username.ToLower() == "admin" || model.Username.ToLower() == "administrator")
            {
                return RedirectToAction("Options", "AdminPanel");
            } else if (model.Username.ToLower() == "kiosk"){
                return RedirectToAction("Index", "Kiosk");
            } else {
                //return RedirectToAction("Report", "RemoteStudent");

                return RedirectToAction("Report", new RouteValueDictionary(
                    new { controller = "RemoteStudent", action = "Report", username = model.Username }));
            }
        }

     
        #endregion
    }
}
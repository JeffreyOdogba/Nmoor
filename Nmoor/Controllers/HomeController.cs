using Nmoor.Models.DataAccessLayer;
using Nmoor.Models.DbContext;
using Nmoor.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nmoor.Controllers
{
    public class HomeController : Controller
    {
        AccountSetUp account;
        [HttpGet]
        public ActionResult Nmoor()
        {
            return View();
        }

        // GET: Home
        [HttpGet]
        public ActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Signup(UserSignUpVM user)
        {
            if (ModelState.IsValid)
            {
                account = new AccountSetUp();
                if (account.RegisterUser(user))
                {
                    ModelState.Clear();
                    ViewBag.Msg = "🥳 Account created try logging in. Happy Transactions!!!";
                }
                else
                {
                    ModelState.Clear();
                    ViewBag.Msg = "😢 Sorry account unavailable";
                }

            }

            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel login)
        {
            if (ModelState.IsValid)
            {
                account = new AccountSetUp();
                if (account.LoginUser(login))
                {
                    Session["username"] = login.Username;
                    RedirectToAction("Main","Dashboard");
                }
                else
                {
                    ModelState.Clear();
                    ViewBag.error = "😮 Something is wrong Email or Password?";
                }
            }
            return View();
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Signup");
        }
             
    }
}
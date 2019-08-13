using Nmoor.Models.DataAccessLayer;
using Nmoor.Models.DbContext;
using Nmoor.Models.Hash;
using Nmoor.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
                  return RedirectToAction("Main", "Dashboard");
                }
                else
                {
                    ModelState.Clear();
                    ViewBag.error = "😮 Something is wrong Email or Password?";
                }
            }
            return View();
        }

        public ActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ForgotPassword(ForgetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (NmoorEntity db = new NmoorEntity())
                {
                    var user = db.User.Where(u => u.email == model.Email).SingleOrDefault();
                    if (user != null)
                    {
                        user.password = Security.Hash(model.ConfirmPassword);
                        db.Entry(user).State = EntityState.Modified;
                        db.SaveChanges();
                        TempData["Worked"] = "Password Changed";
                    }
                }   
                
            }
            return View();
        }


        public ActionResult Logout()
        {
            using (NmoorEntity db = new NmoorEntity())
            {
                var userSession = Session["username"].ToString();
               var user = db.User.Where(u => u.username == userSession).FirstOrDefault();
                user.recentsignin = DateTime.Now;

                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
            }
            Session.Abandon();
            return RedirectToAction("Nmoor");
        }
             
    }
}
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
        // GET: Home
        [HttpGet]
        public ActionResult Nmoor()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Nmoor(UserSignUpVM user)
        {
            account = new AccountSetUp();
            account.RegisterUser(user);
            return View();
        }
    }
}
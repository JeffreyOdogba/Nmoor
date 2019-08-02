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
    public class DashboardController : Controller
    {
        // GET: Dashboard
        [HttpGet]
        public ActionResult Main()
        {
            using (NmoorEntity db = new NmoorEntity())
            {
                    if (Session["username"] == null)
                    {
                        return View("~/Views/Home/Login.cshtml");
                    }

                    var account = from u in db.User.ToList()
                                  where u.username == Session["username"].ToString()
                                  select u;

                    return View(account);               
            }
        }

        [HttpPost]
        public ActionResult Main(DepositViewModel deposit)
        {            
            deposit.Username = Session["username"].ToString();
            Banking.Deposit(deposit);
            
            return View();
        }
    }
}
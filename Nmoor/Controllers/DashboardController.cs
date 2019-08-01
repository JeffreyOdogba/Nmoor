using Nmoor.Models.DbContext;
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
        public ActionResult Main()
        {
            return View();
        }

        public PartialViewResult AccountInfo()
        {
            using (NmoorEntity db = new NmoorEntity())
            {
                var account = db.User.Where(u => u.username == Session["username"].ToString()).ToList();
                //User user = new User();
                //user.balance = account.balance;
                //user.email = account.email;
                //user.token = account.token;
                //user.recentsignin = account.recentsignin;
                //user.fullname = account.fullname;

                return PartialView(account);
            }
            
        }
    }
}
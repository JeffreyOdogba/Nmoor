﻿using Nmoor.Models.DataAccessLayer;
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
        public ActionResult Deposit(DepositViewModel deposit)
        {            
            deposit.Username = Session["username"].ToString();
            if (Banking.Deposit(deposit))
            {
                TempData["Msg"] = "🎉 Deposit Successful !!!";
                return RedirectToAction("Main");
            }

            TempData["Msg"] = "😢 Oops! Something went wrong please contact your Financial Institution";
            return RedirectToAction("Main");
        }

        [HttpPost]
        public ActionResult Withdraw(Withdraw withdraw)
        {
            withdraw.Username = Session["username"].ToString();
            if (Banking.WithDraw(withdraw))
            {
                TempData["Msg"] = "🎉 Withdraw Successful !!!";
                return RedirectToAction("Main");
            }
            TempData["Msg"] = "😢 Oops! Something went wrong please try again later.";
            return RedirectToAction("Main");
        }

        public ActionResult SendTransfar(TransferViewModel transfer)
        {
            transfer.SenderUsername = Session["username"].ToString();            
            if (Banking.SendTransfer(transfer))
            {
                TempData["Msg"] = "🎉 Transfer In-progress!!!";
                return RedirectToAction("Main");
            }
            TempData["Msg"] = "😢 Sorry transaction failed *Click Back-arrow in brawers to Try-Again*.";
            return RedirectToAction("Main");
        }
    }
}
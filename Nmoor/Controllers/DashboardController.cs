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
        /// <summary>
        /// Main was used for display records for current user 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Main()
        {
           Service.ServiceClient service = new Service.ServiceClient();
            using (NmoorEntity db = new NmoorEntity())
            {                
                    if (Session["username"] == null)
                    {
                        return View("~/Views/Home/Login.cshtml");
                    }
                else
                {
                    var account = from u in db.User.ToList()
                                  where u.username == Session["username"].ToString()
                                  select u;                    
                    var userSession = Session["username"].ToString();
                    var recent = Banking.RecentActivity(userSession);

                    var user =  Session["username"].ToString();
                    var amount = db.User.Where(u => u.username == user).SingleOrDefault();
                    decimal n = amount.balance.Value;
                    var converted = service.convertCurrency(n, "USD");
                    ViewBag.converted = string.Format("{0:C}", converted) ;

                    ViewBag.recent = recent;
                    return View(account);
                }                                
            }
        }

        /// <summary>
        /// Deposit get the card information and amount to be added to user
        /// </summary>
        /// <param name="deposit"> gets all html input form</param>
        /// <returns></returns>
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

        /// <summary>
        /// Withdraw substract balance from use
        /// </summary>
        /// <param name="withdraw">gets all html input form</param>
        /// <returns></returns>

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

        /// <summary>
        /// This sends money to another user using the secured token (with the method Banking.SenderUsername)
        /// </summary>
        /// <param name="transfer"></param>
        /// <returns></returns>
        public ActionResult SendTransfer(TransferViewModel transfer)
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

        /// <summary>
        /// ViewTransfer view current transfer sent from other user
        /// </summary>
        /// <returns></returns>
        public ActionResult ViewTransfer()
        {
            if (Session["username"] == null)
            {
                return View("~/Views/Home/Login.cshtml");
            }
            var user = Session["username"].ToString();
            var all = Banking.ShowCurrentTransfar(user);
            ViewData["all"] = all;
            return View(all);
        }

        /// <summary>
        /// the function is used when the user accept money from sender
        /// </summary>
        /// <param name="id">gets the id from the listed transers</param>
        /// <returns></returns>
        public ActionResult Accept(int id)
        {
            var senderUsername = Session["username"].ToString();
            if (Banking.ReceieveTransfer(senderUsername, id))
            {
                TempData["Msg"] = "😎 Money deposited to your account shortly...";
                return RedirectToAction("Main");
            }
            TempData["Msg"] = "😢 Sorry transaction failed";
            return RedirectToAction("Main");
        }
    }
}
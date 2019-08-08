using Nmoor.Models.DbContext;
using Nmoor.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Nmoor.Models.DataAccessLayer
{
    public class Banking
    {
        public static bool Deposit(DepositViewModel deposit)
        {
            bool flag = false;
            if (deposit.Amount <=0)
            {
                flag = false;
            }
            else
            {
                flag = true;
                using (NmoorEntity db = new NmoorEntity())
                {
                    Transaction transaction = new Transaction();
                    var amount = db.User.Where(u => u.username.Equals(deposit.Username)).FirstOrDefault();
                    amount.balance += deposit.Amount;

                    transaction.username = deposit.Username;
                    transaction.transactionType = "Deposit";
                    transaction.amount = deposit.Amount;
                    transaction.dateOfTransaction = DateTime.Now;
                    transaction.timeOfTransaction = TimeSpan.FromSeconds(60);
                    transaction.status = "Success";
                    db.Transaction.Add(transaction);

                    db.Entry(amount).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            return flag;
        }

        public static bool WithDraw(Withdraw withdraw)
        {
            bool flag = false;

            using (NmoorEntity db = new NmoorEntity())
            {
                var amount = db.User.Where(u => u.username.Equals(withdraw.Username)).FirstOrDefault();
                if (withdraw.Amount > amount.balance)
                {
                    flag = false;
                }
                else
                {                    
                    flag = true;
                    Transaction transaction = new Transaction();
                    amount.balance -= withdraw.Amount;
                    db.Entry(amount).State = EntityState.Modified;

                    transaction.username = withdraw.Username;
                    transaction.transactionType = "Withdraw";
                    transaction.amount = withdraw.Amount;
                    transaction.dateOfTransaction = DateTime.Now;
                    transaction.timeOfTransaction = TimeSpan.FromSeconds(60);
                    transaction.status = "Success";
                    db.Transaction.Add(transaction);
                    
                    db.SaveChanges();
                }
            }
            return flag;
        }

        public static bool SendTransfer(TransferViewModel transfer)
        {
            bool flag = false;
            using (NmoorEntity db = new NmoorEntity())
            {
                
               var getToken =  db.User.Where(u => u.token.Equals(transfer.Token)).FirstOrDefault();
                var getSenderUser = db.User.Where(u => u.username.Equals(transfer.SenderUsername)).FirstOrDefault();
                if (getToken != null)
                {
                    var amount = transfer.Amount + 1.99m;
                    if (amount <= getSenderUser.balance)
                    {
                        flag = true;
                        getSenderUser.balance -= amount;

                        Invoice invoice = new Invoice();
                        invoice.senderUsername = transfer.SenderUsername;
                        invoice.receiverUsername = transfer.Token;
                        invoice.amount = amount;
                        invoice.status = "Pending";
                        invoice.date = DateTime.Now;
                        invoice.time = TimeSpan.FromSeconds(60);

                        db.Invoice.Add(invoice);

                        db.SaveChanges();
                    }
                }
            }
            return flag;
        }

        public static bool ReceieveTransfer(string username, int id)
        {
            bool flag = false;

            using (NmoorEntity db = new NmoorEntity())
            {
                flag = true;
                var getAmount = db.Invoice.Find(id);

                if (getAmount.amount > 0)
                {
                    var addAmount = db.User.Where(u => u.username == username).FirstOrDefault();
                    addAmount.balance += getAmount.amount;
                    getAmount.status = "Completed";
                    db.Entry(addAmount).State = EntityState.Modified;
                    db.SaveChanges();
                }               
            }
            return flag;
        }

        public static List<Invoice> ShowCurrentTransfar(string username)
        {
            //List<Invoice> invoices = new List<Invoice>();
            using (NmoorEntity db = new NmoorEntity())
            {
                var user = db.User.Where(u => u.username.Equals(username)).FirstOrDefault();
                var getUserToken = from u in db.Invoice
                                   where u.receiverUsername == user.token && u.status == "Pending"
                                   select u;

                return getUserToken.ToList();
            }
        }

        /// <summary>
        /// Add user query for each user
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public static List<Invoice> RecentActivity(string username)
        {
            using (NmoorEntity db = new NmoorEntity())
            {
                var user = db.User.Where(u => u.username.Equals(username)).FirstOrDefault();
                var getUserToken = from u in db.Invoice
                                   where u.status == "Completed" && user.token == u.receiverUsername || u.senderUsername == user.username
                                   select u;

                return getUserToken.ToList();
            }
        }
    }
}
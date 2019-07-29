using Nmoor.Models.DbContext;
using Nmoor.Models.Hash;
using Nmoor.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nmoor.Models.DataAccessLayer
{
    public class AccountSetUp
    {
        public void RegisterUser(UserSignUpVM signUp)
        {
            using (NmoorEntity db = new NmoorEntity())
            {
                User user = new User()
                {
                    username = signUp.Username,
                    password = Security.Hash(signUp.ConfirmPassword),
                    balance = signUp.Balance,
                    status = "Active",
                    token = Token.TokenGenerator(),
                    email = signUp.Email,
                    signupdate = signUp.SignUpDate,
                    recentsignin = signUp.SignUpDate,
                    fullname = signUp.FullName
                };
                db.User.Add(user);
                db.SaveChanges();
            }

        }
    }
}
﻿using Nmoor.Models.DbContext;
using Nmoor.Models.Hash;
using Nmoor.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;

namespace Nmoor.Models.DataAccessLayer
{
    public class AccountSetUp
    {
        //This accepts the input from controller and add new user
        public bool RegisterUser(UserSignUpVM signUp)
        {
            bool flag = false;
            try
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
                        signupdate = DateTime.Now,
                        recentsignin = DateTime.Now,
                        fullname = signUp.FullName
                    };
                    var aUser = db.User.Any(u => u.username.Equals(user.username) && u.email.Equals(user.email));
                    if (aUser)
                    {
                        flag = false;
                    }
                    else
                    {
                        flag = true;
                        db.User.Add(user);
                        db.SaveChanges();
                    }
                    
                }
                
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        System.Console.WriteLine("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
            }
            return flag;
        }

        // LoginUser check if user exist in the database
        public bool LoginUser(LoginViewModel login)
        {
            bool flag = false;
            using (NmoorEntity db = new NmoorEntity())
            {
                var checkLoginValue = db.User.Where(u => u.username.Equals(login.Username)).FirstOrDefault();
                if (checkLoginValue !=null)
                {
                    if (Security.VerifyPassword(login.Password, checkLoginValue.password))
                    {
                        flag = true;
                    }                    
                }
                else
                {
                    flag = false;
                }
                return flag;
            }
        }
    }
}
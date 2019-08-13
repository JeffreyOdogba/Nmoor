using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Nmoor.Models.ViewModels
{
    public class UserSignUpVM
    {

        [Required]
        [Display(Name = "Full name")]
        public string FullName { get; set; }
        [Required]
        [Display(Name = "Username")]
        public string Username { get; set; }
        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        public string token { get; set; }

        private DateTime signupdate { get; set; }
        public DateTime SignUpDate
        {
            get
            {
                return signupdate;
            }
            set
            {
                signupdate = DateTime.Now;
            }
        }

        private decimal balance;
        public decimal Balance
        {
            get
            {
                return balance;
            }

            set
            {
                balance += 0.00m;
            }

        }
        public string Status { get; set; }
        public string Email { get; set; }

        public UserSignUpVM()
        {

        }

        public UserSignUpVM(string fn, string user, string pass, string tok, DateTime signdate, decimal bal, string stat, string email)
        {
            FullName = fn;
            Username = user;
            ConfirmPassword = pass;
            token = tok;
            SignUpDate = signdate;
            balance = bal;
            Status = stat;
            Email = email;
        }

        
    }
}
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
        [StringLength(255, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 5)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        [Required]
        [StringLength(255, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 5)]
        [Display(Name = "Confirm Password")]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        private string token;
        public string Token
        {
            get
            {
                return token;
            }

            set
            {
                token = TokenGenerator();
            }
        }

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
                balance += 0;
            }

        }
        public string Status { get; set; }
        public string Email { get; set; }

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

        public static string TokenGenerator()
        {
            int size = 15;
            var charSet = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNPQRSTUVWXYZ23456789";
            var chars = charSet.ToCharArray();
            var data = new byte[1];
            var crypto = new RNGCryptoServiceProvider();
            crypto.GetNonZeroBytes(data);
            data = new byte[size];
            crypto.GetNonZeroBytes(data);
            var result = new StringBuilder(size);
            foreach (var b in data)
            {
                result.Append(chars[b % (chars.Length)]);
            }
            return result.ToString();
        }
    }
}
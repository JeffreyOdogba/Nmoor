using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Nmoor.Models.ViewModels
{
    public class Withdraw
    {
        public string Username { get; set; }

        [Required]
        [Display(Name = "Amount")]
        public decimal Amount { get; set; }

        [Required]
        [Display(Name = "Account number")]
        [MaxLength(7)]
        public string BankAccount { get; set; }

        [Required]
        [Display(Name = "Transit number")]
        [RegularExpression(@"/ ^[0 - 9]{5}$/", ErrorMessage = "😢 Expiry Date should be like on your card")]
        public DateTime TransitNumber { get; set; }

        [Required]
        [MaxLength(3)]
        [Display(Name = "Institution number")]
        [RegularExpression("/^[0-9]{3}$/", ErrorMessage = "😢 3 Numbers only")]
        public int InstitutionNumber { get; set; }
    }
}
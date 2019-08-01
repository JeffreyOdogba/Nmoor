using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Nmoor.Models.ViewModels
{
    public class DepositViewModel
    {        
        public string Username { get; set; }

        [Required]
        [Display(Name = "Amount")]
        public decimal Amount { get; set; }

        [Required]
        [Display(Name = "Credit Card Number")]
        [DataType(DataType.CreditCard)]
        public string CardNumber { get; set; }

        [Required]
        [Display(Name = "Expiry Date")]
        [RegularExpression(@"^(0[1-9]|1[0-2]|[1-9])\/(1[4-9]|[2-9][0-9]|20[1-9][1-9])$", ErrorMessage ="😢 Expiry Date should be like on your card")]
        public DateTime ExpiryDate { get; set; }

        [Required]
        [MaxLength(3)]
        [Display(Name = "CVV")]
        [RegularExpression("/^[0-9]{3}$/", ErrorMessage = "😢 3 Numbers only")]
        public int CVV { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Nmoor.Models.ViewModels
{
    public class TransferViewModel
    {
        [Display(Name="Amount")]
        public decimal Amount { get; set; }

        public string Token { get; set; }
        public string SenderUsername { get; set; }
        public string ReceiverUsername { get; set; }

    }
}
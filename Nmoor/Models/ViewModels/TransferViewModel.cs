using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nmoor.Models.ViewModels
{
    public class TransferViewModel
    {
        public decimal Amount { get; set; }
        public string Token { get; set; }
        public string SenderUsername { get; set; }
        public string ReceiverUsername { get; set; }

    }
}
using Nmoor.Models.DbContext;
using Nmoor.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nmoor.Models
{
    public class ToPage
    {
        public User user { get; set; }
        public DepositViewModel deposit{ get; set; }

        public ToPage()
        {
            user = new User();
            deposit = new DepositViewModel();
        }
    }
}
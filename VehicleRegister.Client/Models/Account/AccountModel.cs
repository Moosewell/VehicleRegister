using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VehicleRegister.Client.Models.Account
{
    public class AccountModel
    {
        public string Username { get; set; }
        public string Authorization { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleRegister.Domain.Interfaces;

namespace VehicleRegister.Domain.Classes
{
    public class Account : IAccount
    {
        public Account(string username, string authorization, string password)
        {
            this.username = username;
            this.authorization = authorization;
            this.password = password;
        }
        private string username { get; set; }
        private string authorization { get; set; }
        private string password { get; set; }

        public string Username => username;
        public string Authorization => authorization;
        public string Password => password;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleRegister.Domain.Interfaces;

namespace VehicleRegister.Domain.Classes
{
    public class Service : IService
    {
        public Service(DateTime date, string description)
        {
            this.date = date;
            this.description = description;
        }

        private DateTime date { get; set; }
        private string description { get; set; }

        public DateTime Date => date;
        public string Description => description;
    }
}

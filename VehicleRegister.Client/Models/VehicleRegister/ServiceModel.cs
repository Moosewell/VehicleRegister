using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VehicleRegister.Client.Models.VehicleRegister
{
    public class ServiceModel
    {
        public DateTime Date { get; set; }
        public string Description { get; set; }

        public ServiceModel(DateTime date, string description)
        {
            this.Date = date;
            this.Description = description;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleRegister.DTO.dto
{
    public class ServiceRequest
    {
        public string registrationNumber { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
    }
}

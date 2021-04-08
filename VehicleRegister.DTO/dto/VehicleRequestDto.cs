using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleRegister.DTO.dto
{
    public class VehicleRequestDto
    {
        public string RegistrationNumber { get; set; }
        public string Model { get; set; }
        public string Brand { get; set; }
        public double Weight { get; set; }
        public DateTime FirstTimeInTraffic { get; set; }
        public bool IsRegistered { get; set; }
        public ServiceDto BookedService { get; set; }
        public List<ServiceDto> ServiceHistory { get; set; } = new List<ServiceDto>();
    }

    public class ServiceDto
    {
        public DateTime Date { get; set; }
        public string Description { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleRegister.DTO.dto;

namespace VehicleRegister.Domain.Interfaces
{
    interface IVehicleService
    {
        IVehicle CreateVehicle(VehicleRequest vehicleRequest);
        List<IVehicle> GetVehicleList();
        IVehicle GetVehicle(string registrationNumber);
        
    }
}

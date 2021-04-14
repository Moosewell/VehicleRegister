using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleRegister.DTO.dto;

namespace VehicleRegister.Domain.Interfaces
{
    public interface IVehicleService
    {
        List<IVehicle> Search(List<IVehicle> vehicleList, string search);
    }
}

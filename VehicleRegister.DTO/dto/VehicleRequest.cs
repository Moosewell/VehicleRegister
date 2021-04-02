using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleRegister.DTO.dto
{
    public class VehicleRequest
    {
        string RegistrationNumber { get; }
        string Model { get; }
        double Weight { get; }
        bool IsRegistered { get; }
    }
}

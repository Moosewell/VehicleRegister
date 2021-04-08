using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleRegister.Domain.Interfaces;

namespace VehicleRegister.Repository.Interfaces
{
    public interface IVehicleRepository
    {
        void RegisterVehicle(IVehicle vehicle);
        List<IVehicle> GetAllVehicles();
        IVehicle GetVehicle(string registrationNumber);
        void UpdateVehicle(IVehicle vehicle);
        void DeleteVehicle(string registrationNumber);
        void CompleteService(string registrationNumber);
        void BookService(IVehicle vehicle);
    }
}

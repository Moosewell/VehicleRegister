using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleRegister.Domain.Interfaces
{
    public interface IVehicleFactory
    {
        IVehicle CreateVehicle(string registrationNumber, string model, string brand, double weight, DateTime firstTimeInTraffic,
                       bool isRegistered, IService bookedService, IList<IService> serviceHistory);
        IService CreateService(DateTime date, string description);
    }
}

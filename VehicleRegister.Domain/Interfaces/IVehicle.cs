using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleRegister.Domain.Interfaces
{
    public interface IVehicle
    {
        string RegistrationNumber { get; }
        string Model { get; }
        string Brand { get; }
        double Weight { get; }
        DateTime FirstTimeInTraffic { get; }
        bool IsRegistered { get; }
        IService BookedService { get; }
        IList<IService> ServiceHistory { get; }
        double YearlyFee { get; }
        void BookNewService(IService service);
    }
}

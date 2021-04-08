using System;
using System.Collections.Generic;
using VehicleRegister.Domain.Classes.Vehicles;
using VehicleRegister.Domain.Interfaces;

namespace VehicleRegister.Domain.Classes
{
    public class VehicleFactory
    {
        public IVehicle CreateVehicle(string registrationNumber, string model, string brand, double weight, DateTime firstTimeInTraffic,
                       bool isRegistered, IService bookedService, IList<IService> serviceHistory)
        {
            switch (weight)
            {
                 case double w when (w > 1800 && w < 2500):
                     return new MediumSizedPassengerCar(registrationNumber, model, brand, weight, firstTimeInTraffic,
                                                 isRegistered, bookedService, serviceHistory);

                 case double w when (w > 2500):
                     return new HeavyVehicle(registrationNumber, model, brand, weight, firstTimeInTraffic,
                                                 isRegistered, bookedService, serviceHistory);

                default:
                    return new LightPassengerCar(registrationNumber, model, brand, weight, firstTimeInTraffic, 
                                                 isRegistered, bookedService, serviceHistory);
            }
        }

        public IService CreateService(DateTime date, string description)
        {
            return new Service(date, description);
        }
    }
}

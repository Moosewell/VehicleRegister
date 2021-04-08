using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleRegister.Domain.Interfaces;

namespace VehicleRegister.Domain.Classes.Vehicles
{
    public class HeavyVehicle : Vehicle, IVehicle
    {
        public HeavyVehicle(string registrationNumber, string model, string brand, double weight, DateTime firstTimeInTraffic,
                       bool isRegistered, IService bookedService, IList<IService> serviceHistory)
        {
            this.registrationNumber = registrationNumber;
            this.model = model;
            this.brand = brand;
            this.weight = weight;
            this.firstTimeInTraffic = firstTimeInTraffic;
            this.isRegistered = isRegistered;
            this.bookedService = bookedService;
            this.serviceHistory = serviceHistory;
            this.yearlyFee = 1800;
        }

        private string registrationNumber { get; set; }
        private string model { get; set; }
        private string brand { get; set; }
        private double weight { get; set; }
        private DateTime firstTimeInTraffic { get; set; }
        private bool isRegistered { get; set; }
        private IService bookedService { get; set; }
        private IList<IService> serviceHistory { get; set; }
        private double yearlyFee { get; set; }

    }
}

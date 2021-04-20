using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleRegister.Domain.Interfaces;

namespace VehicleRegister.Domain.Classes.Vehicles
{
    public class LightPassengerCar : IVehicle
    {
        public LightPassengerCar(string registrationNumber, string model, string brand, double weight, DateTime firstTimeInTraffic,
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
            this.yearlyFee = 1200;
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

        public string RegistrationNumber => registrationNumber;
        public string Model => model;
        public string Brand => brand;
        public double Weight => weight;
        public DateTime FirstTimeInTraffic => firstTimeInTraffic;
        public bool IsRegistered => isRegistered;
        public IService BookedService => bookedService;
        public IList<IService> ServiceHistory => serviceHistory;
        public double YearlyFee => yearlyFee;

        public void BookNewService(IService service)
        {
            bookedService = service;
        }
    }
}

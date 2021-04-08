﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleRegister.Domain.Classes;
using VehicleRegister.Domain.Interfaces;
using VehicleRegister.Repository.Interfaces;

namespace VehicleRegister.Repository.Classes
{
    public class AzureRepository : IVehicleRepository
    {
        private readonly AzureDataContext dataContext;

        public AzureRepository()
        {
            dataContext = new AzureDataContext();
        }

        public void DeleteVehicle(string registrationNumber)
        {
            var dataBaseVehicle = dataContext.Vehicles.Where(o => o.RegistrationNumber == registrationNumber).FirstOrDefault();
            dataContext.Vehicles.DeleteOnSubmit(dataBaseVehicle);

            var databaseVehicleServiceList = dataContext.VehicleServices.Where(o => o.RegistrationNumber == registrationNumber);
            foreach (var vehicleService in databaseVehicleServiceList)
            {
                dataContext.VehicleServices.DeleteOnSubmit(vehicleService);
            }

            var serviceIdList = databaseVehicleServiceList.Select(o => o.ServiceId);
            foreach (var id in serviceIdList)
            {
                var service = dataContext.Services.Where(o => o.Id == id).FirstOrDefault();
                dataContext.Services.DeleteOnSubmit(service);
            }

            dataContext.SubmitChanges();
        }

        public List<IVehicle> GetAllVehicles()
        {
            List<IVehicle> vehicleList = new List<IVehicle>();

            foreach(var entity in dataContext.Vehicles)
            {
                vehicleList.Add(GetVehicle(entity.RegistrationNumber));
            }

            return vehicleList;
        }

        public IVehicle GetVehicle(string registrationNumber)
        {
            VehicleFactory factory = new VehicleFactory();
            var dataBaseVehicle = dataContext.Vehicles.Where(o => o.RegistrationNumber == registrationNumber).FirstOrDefault();

            var databaseVehicleServiceList = dataContext.VehicleServices.Where(o => o.RegistrationNumber == registrationNumber);
            var serviceIdList = databaseVehicleServiceList.Select(o => o.ServiceId);
            List<IService> serviceList = new List<IService>();
            IService bookedService = null;
            foreach (var id in serviceIdList)
            {
                var databaseService = dataContext.Services.Where(o => o.Id == id).FirstOrDefault();
                if (databaseService.Completed == false)
                {
                    bookedService = factory.CreateService(databaseService.Date, databaseService.Description);
                }
                serviceList.Add(factory.CreateService(databaseService.Date, databaseService.Description));
            }

            IVehicle vehicle = factory.CreateVehicle(dataBaseVehicle.RegistrationNumber, dataBaseVehicle.Model,
                                                     dataBaseVehicle.Brand, dataBaseVehicle.Weight,
                                                     (DateTime)dataBaseVehicle.FirstTimeInTraffic, 
                                                     dataBaseVehicle.IsRegistered, bookedService, serviceList);
            return vehicle;
        }

        private string AddVehicle(IVehicle vehicle)
        {
            var newVehicle = new Vehicle
            {
                RegistrationNumber = vehicle.RegistrationNumber,
                Model = vehicle.Model,
                Brand = vehicle.Brand,
                TypeOfVehicle = vehicle.GetType().ToString(),
                Weight = vehicle.Weight,
                FirstTimeInTraffic = vehicle.FirstTimeInTraffic,
                IsRegistered = vehicle.IsRegistered,
            };
            dataContext.Vehicles.InsertOnSubmit(newVehicle);
            dataContext.SubmitChanges();

            return newVehicle.RegistrationNumber;
        }

        private int AddService(string registrationNumber, IService service, bool isCompleted)
        {
            var newbookedService = new Service
            {
                Date = service.Date,
                Description = service.Description,
                Completed = isCompleted
            };
            dataContext.Services.InsertOnSubmit(newbookedService);
            dataContext.SubmitChanges();

            var newVehicleService = new VehicleService
            {
                RegistrationNumber = registrationNumber,
                ServiceId = newbookedService.Id
            };
            dataContext.VehicleServices.InsertOnSubmit(newVehicleService);
            dataContext.SubmitChanges();

            return newVehicleService.ServiceId;
        }

        public void RegisterVehicle(IVehicle vehicle)
        {
            var registrationNumber = AddVehicle(vehicle);

            if (vehicle.BookedService != null)
                AddService(registrationNumber, vehicle.BookedService, false);

            foreach (IService service in vehicle.ServiceHistory)
                AddService(registrationNumber, service, true);
        }

        public void UpdateVehicle(IVehicle vehicle)
        {
            var currentVehicle = dataContext.Vehicles.Where(o => o.RegistrationNumber == vehicle.RegistrationNumber).FirstOrDefault();
            if (currentVehicle == null)
                throw new Exception("No vehicle found in database");

            currentVehicle.Model = vehicle.Model;
            currentVehicle.Brand = vehicle.Brand;
            currentVehicle.Weight = vehicle.Weight;
            currentVehicle.FirstTimeInTraffic = vehicle.FirstTimeInTraffic;
            currentVehicle.IsRegistered = vehicle.IsRegistered;
        }

        public void BookService(IVehicle vehicle)
        {
            AddService(vehicle.RegistrationNumber, vehicle.BookedService, false);
        }

        public void CompleteService(string registrationNumber)
        {
            var databaseVehicleServiceList = dataContext.VehicleServices.Where(o => o.RegistrationNumber == registrationNumber);
            var serviceIdList = databaseVehicleServiceList.Select(o => o.ServiceId);
            foreach(var id in serviceIdList)
            {
                var service = dataContext.Services.Where(o => o.Id == id).FirstOrDefault();
                if (service.Completed == false)
                    service.Completed = true;
            }
        }
    }
}
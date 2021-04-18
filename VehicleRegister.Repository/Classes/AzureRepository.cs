using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleRegister.Domain.Classes;
using VehicleRegister.Domain.Interfaces;
using VehicleRegister.Repository.Interfaces;

namespace VehicleRegister.Repository.Classes
{
    public class AzureRepository : IVehicleRepository, IAccountRepository
    {
        private readonly AzureDataContext dataContext;

        public AzureRepository()
        {
            dataContext = new AzureDataContext();
        }

        public void DeleteVehicle(string registrationNumber)
        {
            var dataBaseVehicle = dataContext.Vehicles.Where(o => o.RegistrationNumber.Equals(registrationNumber)).FirstOrDefault();
            if (dataBaseVehicle == null)
                throw new Exception("No vehicle found in database");

            dataContext.Vehicles.DeleteOnSubmit(dataBaseVehicle);

            var databaseVehicleServiceList = dataContext.VehicleServices.Where(o => o.RegistrationNumber == registrationNumber);
            foreach (var vehicleService in databaseVehicleServiceList)
            {
                dataContext.VehicleServices.DeleteOnSubmit(vehicleService);
            }

            var serviceIdList = databaseVehicleServiceList.Select(o => o.ServiceId);
            foreach (var id in serviceIdList)
            {
                var service = dataContext.Services.Where(o => o.Id == id).Single();
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

        private List<int> GetServiceDatabaseIDListForVehicle(string registrationNumber)
        {
            var databaseVehicleServiceList = dataContext.VehicleServices.Where(o => o.RegistrationNumber == registrationNumber).ToList();
            var serviceIdList = databaseVehicleServiceList.Select(o => o.ServiceId).ToList();

            return serviceIdList;
        }

        public IVehicle GetVehicle(string registrationNumber)
        {
            VehicleFactory factory = new VehicleFactory();
            var dataBaseVehicle = dataContext.Vehicles.Where(o => o.RegistrationNumber == registrationNumber).FirstOrDefault();
            if (dataBaseVehicle == null)
                throw new Exception("No vehicle found in database");

            var serviceIdList = GetServiceDatabaseIDListForVehicle(registrationNumber);
            List<IService> serviceList = new List<IService>();
            IService bookedService = null;
            foreach (var id in serviceIdList)
            {
                var databaseService = dataContext.Services.Where(o => o.Id == id).Single();
                if (databaseService.Completed == false)
                {
                    bookedService = factory.CreateService(databaseService.Date.Date, databaseService.Description);
                }
                else
                { 
                serviceList.Add(factory.CreateService(databaseService.Date.Date, databaseService.Description));
                }
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
                TypeOfVehicle = vehicle.GetType().Name,
                Weight = vehicle.Weight,
                FirstTimeInTraffic = vehicle.FirstTimeInTraffic.Date,
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
                Date = service.Date.Date,
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
            foreach (var entity in dataContext.Vehicles)
            {
                if (entity.RegistrationNumber == vehicle.RegistrationNumber)
                    throw new Exception($"Car with registration number: {vehicle.RegistrationNumber} already exists in database");
            }

            var registrationNumber = AddVehicle(vehicle);

            if (vehicle.BookedService != null)
                AddService(registrationNumber, vehicle.BookedService, false);

            foreach (IService service in vehicle.ServiceHistory)
                AddService(registrationNumber, service, true);
        }

        public void UpdateVehicle(IVehicle vehicle)
        {
            var databaseVehicle = dataContext.Vehicles.Where(o => o.RegistrationNumber == vehicle.RegistrationNumber).FirstOrDefault();
            if (databaseVehicle == null)
                throw new Exception("No vehicle found in database");

            databaseVehicle.Model = vehicle.Model;
            databaseVehicle.Brand = vehicle.Brand;
            databaseVehicle.Weight = vehicle.Weight;
            databaseVehicle.FirstTimeInTraffic = vehicle.FirstTimeInTraffic;
            databaseVehicle.IsRegistered = vehicle.IsRegistered;

            if(vehicle.BookedService != null)
            {
                bool bookedServiceNeedsToBeUpdated = true;
                var serviceIdList = GetServiceDatabaseIDListForVehicle(vehicle.RegistrationNumber);
                foreach (var id in serviceIdList)
                {
                    var databaseService = dataContext.Services.Where(o => o.Id == id).Single();
                    if (databaseService.Completed == false)
                    {
                        databaseService.Date = vehicle.BookedService.Date.Date;
                        databaseService.Description = vehicle.BookedService.Description;
                        bookedServiceNeedsToBeUpdated = false;
                    }
                }
                if(bookedServiceNeedsToBeUpdated)
                {
                    AddService(vehicle.RegistrationNumber, vehicle.BookedService, false);
                }
            }

            dataContext.SubmitChanges();
        }

        public void CompleteService(string registrationNumber)
        {
            var serviceIdList = GetServiceDatabaseIDListForVehicle(registrationNumber);
            foreach (var id in serviceIdList)
            {
                var service = dataContext.Services.Where(o => o.Id == id).Single();
                if (service.Completed == false)
                    service.Completed = true;
            }
            dataContext.SubmitChanges();
        }

        public void RegisterAccount(IAccount account)
        {
            foreach(var entity in dataContext.Accounts)
            {
                if (entity.Username == account.Username)
                    throw new Exception("Username already in use");
            }
            var newAccount = new Account
            {
                Username = account.Username,
                Authorization = account.Authorization,
                Password = account.Password
            };

            dataContext.Accounts.InsertOnSubmit(newAccount);
            dataContext.SubmitChanges();

        }

        public List<IAccount> GetAllAccounts()
        {
            List<IAccount> accountList = new List<IAccount>();
            foreach(var entity in dataContext.Accounts)
            {
                accountList.Add(GetAccount(entity.Username));
            }

            return accountList;
        }

        public IAccount GetAccount(string username)
        {
            var databaseAccount = dataContext.Accounts.Where(o => o.Username == username).FirstOrDefault();
            if (databaseAccount == null)
                throw new Exception("Account not found in database");

            IAccount account = new Domain.Classes.Account(databaseAccount.Username, 
                                                          databaseAccount.Authorization, 
                                                          databaseAccount.Password);
            return account;
        }
    }
}

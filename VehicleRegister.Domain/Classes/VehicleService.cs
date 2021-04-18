using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using VehicleRegister.Domain.Interfaces;
using VehicleRegister.DTO.dto;

namespace VehicleRegister.Domain.Classes
{
    public class VehicleService : IVehicleService
    {
        public List<IVehicle> Search(List<IVehicle> vehicleList, string search)
        {
            if (string.IsNullOrEmpty(search))
                return vehicleList;

            List<IVehicle> newVehicleList = new List<IVehicle>();

            List<List<IVehicle>> Lists = new List<List<IVehicle>>(){
                new List<IVehicle>(),
                new List<IVehicle>(),
                new List<IVehicle>(),
            };

            Lists[0] = vehicleList.Where(o => Regex.IsMatch(o.RegistrationNumber, search, RegexOptions.IgnoreCase)).ToList();
            Lists[1] = vehicleList.Where(o => Regex.IsMatch(o.Model, search, RegexOptions.IgnoreCase)).ToList();
            Lists[2] = vehicleList.Where(o => Regex.IsMatch(o.Brand, search, RegexOptions.IgnoreCase)).ToList();

            newVehicleList = Lists[0].Union(Lists[1]).ToList();
            newVehicleList = newVehicleList.Union(Lists[2]).ToList();

            return newVehicleList;
        }

        public List<IVehicle> BookService(IService service, List<IVehicle> vehicles)
        {
            List<string> vehiclesWithBookings = new List<string>();

            foreach(IVehicle vehicle in vehicles)
            {
                if (vehicle.BookedService != null)
                    vehiclesWithBookings.Add(vehicle.RegistrationNumber);

                vehicle.BookNewService(service);
            }
            if (vehiclesWithBookings.Count > 0)
            {
                string BookedRegistrationNumbers = "";
                for(var i = 0; 0 < vehiclesWithBookings.Count; i++)
                {
                    BookedRegistrationNumbers += vehiclesWithBookings[i];
                    BookedRegistrationNumbers += i + 1 == vehiclesWithBookings.Count ? "" : ",";
                }
                throw new Exception($"Vehicles with following registration numbers already have a service booked: {BookedRegistrationNumbers}");
            }
                

            return vehicles;
        }

    }
}

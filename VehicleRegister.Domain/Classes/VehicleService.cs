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

    }
}

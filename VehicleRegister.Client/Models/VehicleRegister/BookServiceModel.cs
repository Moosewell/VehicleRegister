using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VehicleRegister.DTO.dto;

namespace VehicleRegister.Client.Models.VehicleRegister
{
    public class BookServiceModel
    {
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public string VehicleOneRegistrationNumber {get; set;}
        public string VehicleTwoRegistrationNumber { get; set; }
        public string VehicleThreeRegistrationNumber { get; set; }

        public BookServiceRequestDto GetDto()
        {
            BookServiceRequestDto request = new BookServiceRequestDto();
            request.Date = Date;
            request.Description = Description;
            if(VehicleOneRegistrationNumber != null)
                request.vehicleRegistrationNumbers.Add(VehicleOneRegistrationNumber);

            if (VehicleTwoRegistrationNumber != null)
                request.vehicleRegistrationNumbers.Add(VehicleTwoRegistrationNumber);

            if (VehicleThreeRegistrationNumber != null)
                request.vehicleRegistrationNumbers.Add(VehicleThreeRegistrationNumber);

            return request;
        }
    }
}
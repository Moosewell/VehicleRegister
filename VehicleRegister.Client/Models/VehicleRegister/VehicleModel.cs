using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VehicleRegister.DTO.dto;

namespace VehicleRegister.Client.Models.VehicleRegister
{
    public class VehicleModel
    {
        public string RegistrationNumber {get; set; }
        public string Model { get; set; }
        public string Brand { get; set; }
        public double Weight { get; set; }
        public DateTime FirstTimeInTraffic { get; set; }
        public bool IsRegistered { get; set; }
        public ServiceModel BookedService { get; set; }
        public List<ServiceModel> ServiceHistory { get; set; }
        
        public double YearlyIncome { get; set; }
        public string VehicleType { get; set; }

        public VehicleRequestDto GetDto()
        {
            VehicleRequestDto request = new VehicleRequestDto();
            request.RegistrationNumber = RegistrationNumber;
            request.Model = Model;
            request.Brand = Brand;
            request.Weight = Weight;
            request.FirstTimeInTraffic = FirstTimeInTraffic;
            request.IsRegistered = IsRegistered;

            if(BookedService != null)
                request.BookedService = GetServiceForDto(BookedService);

            if(ServiceHistory != null)
                foreach (ServiceModel service in ServiceHistory)
                    request.ServiceHistory.Add(GetServiceForDto(service));

            return request;
        }

        private ServiceDto GetServiceForDto(ServiceModel serviceModel)
        {
            ServiceDto service = new ServiceDto();
            service.Date = serviceModel.Date;
            service.Description = serviceModel.Description;

            return service;
        }
    }
}
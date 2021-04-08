using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using VehicleRegister.Domain.Classes;
using VehicleRegister.Domain.Interfaces;
using VehicleRegister.DTO.dto;
using VehicleRegister.Repository.Interfaces;

namespace VehicleRegister.API.Controllers
{
    public class VehicleController : ApiController
    {
        private readonly IVehicleService vehicleService;
        private readonly IVehicleRepository vehicleRepository;

        public VehicleController(IVehicleService vehicleService, IVehicleRepository vehicleRepository)
        {
            this.vehicleService = vehicleService;
            this.vehicleRepository = vehicleRepository;
        }

        private IVehicle CreateVehicleFromDto(VehicleRequestDto vehicleRequestDto)
        {
            var factory = new VehicleFactory();
            IList<IService> vehicleServices = new List<IService>();

            foreach (var service in vehicleRequestDto.ServiceHistory)
                vehicleServices.Add(factory.CreateService(service.Date, service.Description));

            IService bookedService = factory.CreateService(vehicleRequestDto.BookedService.Date,
                                                           vehicleRequestDto.BookedService.Description);

            IVehicle vehicle = factory.CreateVehicle(vehicleRequestDto.RegistrationNumber, vehicleRequestDto.Model,
                                      vehicleRequestDto.Brand, vehicleRequestDto.Weight,
                                      vehicleRequestDto.FirstTimeInTraffic, vehicleRequestDto.IsRegistered,
                                      bookedService, vehicleServices);
            return vehicle;
        }

       [HttpPost]
       [Route("RegisterVehicle")]
       public IHttpActionResult RegisterVehicle(VehicleRequestDto vehicleRequestDto)
        {
            var vehicle = CreateVehicleFromDto(vehicleRequestDto);

            vehicleRepository.RegisterVehicle(vehicle);
            return Ok();
        }

        [HttpGet]
        [Route("GetVehicleList")]
        public IHttpActionResult GetVehicleList()
        {
            return Ok(vehicleRepository.GetAllVehicles()); 
        }

        [HttpGet]
        [Route("GetVehicle")]
        public IHttpActionResult GetVehicle(string registrationNumber)
        {
            return Ok(vehicleRepository.GetVehicle(registrationNumber)); 
        }

        [HttpPost]
        [Route("RemoveVehicle")]
        public IHttpActionResult RemoveVehicle(string registrationNumber)
        {
            vehicleRepository.DeleteVehicle(registrationNumber);
            return Ok(); 
        }

        [HttpPost]
        [Route("UpdateVehicle")]
        public IHttpActionResult UpdateVehicle(VehicleRequestDto vehicleRequestDto)
        {
            var vehicle = CreateVehicleFromDto(vehicleRequestDto);

            vehicleRepository.UpdateVehicle(vehicle);
            return Ok();
        }

        [HttpPost]
        [Route("BookService")]
        public IHttpActionResult BookService(VehicleRequestDto vehicleRequestDto)
        {
            var vehicle = CreateVehicleFromDto(vehicleRequestDto);

            vehicleRepository.BookService(vehicle);
            return Ok();
        }

        [HttpPost]
        [Route("CompleteService")]
        public IHttpActionResult CompleteService(string registrationNumber)
        {
            vehicleRepository.CompleteService(registrationNumber);
            return Ok();
        }

    }
}
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
    [Authorize]
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

            IService bookedService = null;
            if (vehicleRequestDto.BookedService != null)
                bookedService = factory.CreateService(vehicleRequestDto.BookedService.Date,
                                                           vehicleRequestDto.BookedService.Description);

                IVehicle vehicle = factory.CreateVehicle(vehicleRequestDto.RegistrationNumber, vehicleRequestDto.Model,
                                      vehicleRequestDto.Brand, vehicleRequestDto.Weight,
                                      vehicleRequestDto.FirstTimeInTraffic, vehicleRequestDto.IsRegistered,
                                      bookedService, vehicleServices);
            return vehicle;
        }

       [Authorize(Roles = "Super Admin,Admin")]
       [HttpPost]
       [Route("api/registervehicle")]
       public IHttpActionResult RegisterVehicle(VehicleRequestDto vehicleRequestDto)
        {
            var vehicle = CreateVehicleFromDto(vehicleRequestDto);

            vehicleRepository.RegisterVehicle(vehicle);
            return Ok();
        }

        [Authorize(Roles = "Super Admin,Admin")]
        [HttpPost]
        [Route("api/getvehiclelist")]
        public IHttpActionResult GetVehicleList(SearchRequestDto searchRequestDto)
        {
            List<IVehicle> vehicleList = vehicleRepository.GetAllVehicles();
            vehicleList = vehicleService.Search(vehicleList, searchRequestDto.search);
            return Ok(vehicleList);
        }

        [Authorize(Roles = "Super Admin,Admin")]
        [HttpPost]
        [Route("api/getvehicle")]
        public IHttpActionResult GetVehicle(VehicleRequestDto vehicleRequestDto)
        {
            return Ok(vehicleRepository.GetVehicle(vehicleRequestDto.RegistrationNumber)); 
        }

        [Authorize(Roles = "Super Admin,Admin")]
        [HttpPost]
        [Route("api/removevehicle")]
        public IHttpActionResult RemoveVehicle(VehicleRequestDto vehicleRequestDto)
        {
            vehicleRepository.DeleteVehicle(vehicleRequestDto.RegistrationNumber);
            return Ok(); 
        }

        [Authorize(Roles = "Super Admin,Admin")]
        [HttpPost]
        [Route("api/updatevehicle")]
        public IHttpActionResult UpdateVehicle(VehicleRequestDto vehicleRequestDto)
        {
            var vehicle = CreateVehicleFromDto(vehicleRequestDto);

            vehicleRepository.UpdateVehicle(vehicle);
            return Ok();
        }

        [Authorize(Roles = "Super Admin,Admin")]
        [HttpPost]
        [Route("api/bookservice")]
        public IHttpActionResult BookService(BookServiceRequestDto bookServiceRequestDto)
        {
            var factory = new VehicleFactory();
            IService service = factory.CreateService(bookServiceRequestDto.Date, bookServiceRequestDto.Description);

            List<IVehicle> vehicles = new List<IVehicle>();
            foreach (string registrationNumber in bookServiceRequestDto.vehicleRegistrationNumbers)
            {
                vehicles.Add(vehicleRepository.GetVehicle(registrationNumber));
            }

            vehicles = vehicleService.BookService(service, vehicles);

            foreach (IVehicle vehicle in vehicles)
            {
                vehicleRepository.UpdateVehicle(vehicle);
            }
            return Ok();
        }

        [Authorize(Roles = "Super Admin,Admin")]
        [HttpPost]
        [Route("api/completeservice")]
        public IHttpActionResult CompleteService(RegistrationNumberRequestDto registrationNumberRequestDto)
        {
            vehicleRepository.CompleteService(registrationNumberRequestDto.RegistrationNumber);
            return Ok();
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using VehicleRegister.DTO.dto;

namespace VehicleRegister.API.Controllers
{
    public class VehicleServiceConroller : ApiController
    {
       [HttpPost]
       [Route("AddVehicle")]
       public IHttpActionResult AddVehicle(VehicleRequest vehicleRequest)
        {
            //var vehicle = VehicleService.CreateVehicle(vehicleRequest)
            //repository.AddVehicle(vehicle)
            return Ok();
        }

        [HttpGet]
        [Route("GetVehicleList")]
        public IHttpActionResult GetVehicleList()
        {
            return Ok(); //repository.GetVehicleList
        }

        [HttpGet]
        [Route("GetVehicle")]
        public IHttpActionResult GetVehicle(string registrationNumber)
        {
            return Ok(); //repository.GetVehicle(registrationNumber)
        }

        [HttpPost]
        [Route("RemoveVehicle")]
        public IHttpActionResult RemoveVehicle(string registrationNumber)
        {
            //repository.GetVehicle(registrationNumber)
            //repository.RemoveVehicle(IVehicle)
            return Ok(); 
        }

        [HttpPost]
        [Route("UpdateVehicle")]
        public IHttpActionResult UpdateVehicle(VehicleRequest vehicleRequest)
        {
            //var oldVehicle = repository.GetVehicle(vehicleRequest.registrationNumber)
            //var newVehicle = vehicleSerivce.CreateVehicle(vehicleRequest.allVariables)
            //vehicle = vehicleSerivce.UpdateVehicle(oldVehicle, newVehicle) 
            //repository.UpdateVehicle(IVehicle)
            return Ok();
        }

        [HttpPost]
        [Route("BookService")]
        public IHttpActionResult BookService(ServiceRequest serviceRequest)
        {
            //var oldVehicle = repository.GetVehicle(serviceRequest.registrationNumber)
            //var newVehicle = vehicleSerivce.BookService(oldVehicle, serviceRequest.Date, serviceRequest.Description)
            //vehicle = vehicleSerivce.UpdateVehicle(oldVehicle, newVehicle) 
            //repository.UpdateVehicle(IVehicle)
            return Ok();
        }

    }
}
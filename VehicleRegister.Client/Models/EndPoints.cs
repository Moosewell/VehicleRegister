using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace VehicleRegister.Client.Models
{
    public class EndPoints
    {
        string HostName => ConfigurationManager.AppSettings["hostname"];

        public string CreateAccount => HostName + "api/createaccount";
        public string CreateLogin => HostName + "api/login";
        public string RegisterVehicle => HostName + "api/registervehicle";
        public string GetVehicleList => HostName + "api/getvehiclelist";
        public string GetVehicle => HostName + "api/getvehicle";
        public string RemoveVehicle => HostName + "api/removevehicle";
        public string UpdateVehicle => HostName + "api/updatevehicle";
        public string BookService => HostName + "api/bookservice";
        public string CompleteService => HostName + "api/completeservice";
        public string GetToken => HostName + "token";
    }
}
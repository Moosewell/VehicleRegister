using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Web.Mvc;
using VehicleRegister.Client.Models;
using VehicleRegister.Client.Models.VehicleRegister;
using VehicleRegister.DTO.dto;

namespace VehicleRegister.Client.Controllers
{
    public class VehicleRegisterController : Controller
    {
        private readonly EndPoints endPoints;

        public VehicleRegisterController()
        {
            endPoints = new EndPoints();
        }
        public ActionResult HomeView()
        {
            return View("Home");
        }

        public ActionResult RegisterVehicleView()
        {
            return View("RegisterVehicle");
        }

        public ActionResult BookServiceView()
        {
            return View("BookService");
        }
        

        public ActionResult RegisterVehicle(VehicleModel vehicleModel)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", Session["TokenKey"].ToString());
                VehicleRequestDto request = vehicleModel.GetDto();
                var jsonRequestString = JsonConvert.SerializeObject(request);
                var stringContent = new StringContent(jsonRequestString, Encoding.UTF8, "application/json");
                var response = client.PostAsync(endPoints.RegisterVehicle, stringContent).Result;
            }
            ViewBag.message = "Vehicle has been registered!";
            return View("Home");
        }

        public ActionResult GetVehicleList(string search)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", Session["TokenKey"].ToString());
                SearchRequestDto request = new SearchRequestDto();
                request.search = search;
                var jsonRequestString = JsonConvert.SerializeObject(request);
                var stringContent = new StringContent(jsonRequestString, Encoding.UTF8, "application/json");
                var response = client.PostAsync(endPoints.GetVehicleList, stringContent).Result;
                if (response != null)
                {
                    var jsonString = response.Content.ReadAsStringAsync().Result;
                    var vehicles = JsonConvert.DeserializeObject<List<VehicleModel>>(jsonString);
                    string listAsText = "";
                    foreach (VehicleModel vehicle in vehicles)
                    {
                        string vehicleAsText = $"{vehicle.RegistrationNumber} | {vehicle.Model} | {vehicle.Brand}";
                        listAsText += $"{vehicleAsText}\n";
                    }
                    ViewBag.Content = listAsText;
                }
            }
            return View("GetVehicleList");
        }

        public ActionResult GetVehicle(string registrationNumber)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", Session["TokenKey"].ToString());
                VehicleRequestDto request = new VehicleRequestDto();
                request.RegistrationNumber = registrationNumber;
                var jsonRequestString = JsonConvert.SerializeObject(request);
                var stringContent = new StringContent(jsonRequestString, Encoding.UTF8, "application/json");
                var response = client.PostAsync(endPoints.GetVehicle, stringContent).Result;
                if (response != null)
                {
                    var jsonString = response.Content.ReadAsStringAsync().Result;
                    var vehicle = JsonConvert.DeserializeObject<VehicleModel>(jsonString);

                    ViewBag.RegistrationNumber = vehicle.RegistrationNumber;
                    ViewBag.Model = vehicle.Model;
                    ViewBag.Brand = vehicle.Brand;
                    ViewBag.VehicleType = vehicle.GetVehicleType();
                    ViewBag.Weight = vehicle.Weight;
                    ViewBag.FirstTimeInTrafic = vehicle.FirstTimeInTraffic.ToShortDateString();
                    ViewBag.IsRegistered = vehicle.IsRegistered.ToString();

                    if (vehicle.BookedService != null)
                    {
                        ViewBag.Date = vehicle.BookedService.Date.ToShortDateString();
                        ViewBag.Description = vehicle.BookedService.Description;
                    }

                    if (vehicle.ServiceHistory.Count > 0)
                    {
                        string listAsText = "";
                        foreach (ServiceModel service in vehicle.ServiceHistory)
                        {
                            listAsText += $"{service.Date.ToShortDateString()} \n";
                            listAsText += $"{service.Description} \n \n";
                        }
                        ViewBag.ServiceHistory = listAsText;
                    }
                }
            }
            return View("GetVehicle");
        }

        public ActionResult RemoveVehicle(string registrationNumber)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", Session["TokenKey"].ToString());
                VehicleRequestDto request = new VehicleRequestDto();
                request.RegistrationNumber = registrationNumber;
                var jsonRequestString = JsonConvert.SerializeObject(request);
                var stringContent = new StringContent(jsonRequestString, Encoding.UTF8, "application/json");
                var response = client.PostAsync(endPoints.RemoveVehicle, stringContent).Result;
            }
            ViewBag.Message = "Vehicle has been removed!";
            return View("Home");
        }
        public ActionResult UpdateVehicleView(string registrationNumber)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", Session["TokenKey"].ToString());
                VehicleRequestDto request = new VehicleRequestDto();
                request.RegistrationNumber = registrationNumber;
                var jsonRequestString = JsonConvert.SerializeObject(request);
                var stringContent = new StringContent(jsonRequestString, Encoding.UTF8, "application/json");
                var response = client.PostAsync(endPoints.GetVehicle, stringContent).Result;
                if (response != null)
                {
                    var jsonString = response.Content.ReadAsStringAsync().Result;
                    var vehicle = JsonConvert.DeserializeObject<VehicleModel>(jsonString);

                    ViewBag.RegistrationNumber = vehicle.RegistrationNumber;
                    ViewBag.Model = vehicle.Model;
                    ViewBag.Brand = vehicle.Brand;
                    ViewBag.VehicleType = vehicle.GetVehicleType();
                    ViewBag.Weight = vehicle.Weight;
                    var date = vehicle.FirstTimeInTraffic.Date;
                    string year = date.Year.ToString();
                    string month = date.Month < 10 ? "0" + date.Month.ToString() : date.Month.ToString();
                    string day = date.Day < 10 ? "0" + date.Day.ToString() : date.Day.ToString();
                    ViewBag.FirstTimeInTrafic = $"{year}-{month}-{day}";
                    ViewBag.IsRegistered = vehicle.IsRegistered;

                    if (vehicle.BookedService != null)
                    {
                        date = vehicle.BookedService.Date.Date;
                        year = date.Year.ToString();
                        month = date.Month < 10 ? "0" + date.Month.ToString() : date.Month.ToString();
                        day = date.Day < 10 ? "0" + date.Day.ToString() : date.Day.ToString();
                        ViewBag.Date = $"{year}-{month}-{day}";
                        ViewBag.Description = vehicle.BookedService.Description;
                    }
                }
            }
            return View("UpdateVehicle");
        }

        public ActionResult UpdateVehicle(VehicleModel vehicleModel)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", Session["TokenKey"].ToString());
                VehicleRequestDto request = vehicleModel.GetDto();
                var jsonRequestString = JsonConvert.SerializeObject(request);
                var stringContent = new StringContent(jsonRequestString, Encoding.UTF8, "application/json");
                var response = client.PostAsync(endPoints.UpdateVehicle, stringContent).Result;
            }
            ViewBag.Message = "Vehicle has been updated!";
            return View("Home");
        }

        public ActionResult BookService(BookServiceModel bookServiceModel)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", Session["TokenKey"].ToString());
                BookServiceRequestDto request = bookServiceModel.GetDto();
                var jsonRequestString = JsonConvert.SerializeObject(request);
                var stringContent = new StringContent(jsonRequestString, Encoding.UTF8, "application/json");
                var response = client.PostAsync(endPoints.BookService, stringContent).Result;
            }
            ViewBag.Message = "Service has been booked!";
            return View("Home");
        }

        public ActionResult CompleteService(string registrationNumber)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", Session["TokenKey"].ToString());
                RegistrationNumberRequestDto request = new RegistrationNumberRequestDto
                {
                    RegistrationNumber = registrationNumber
                };

                var jsonRequestString = JsonConvert.SerializeObject(request);
                var stringContent = new StringContent(jsonRequestString, Encoding.UTF8, "application/json");
                var response = client.PostAsync(endPoints.CompleteService, stringContent).Result;
            }
            ViewBag.Message = "Service has been completed!";
            return View("Home");
        }
    }
}
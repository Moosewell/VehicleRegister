using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.HtmlControls;
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

        public ActionResult RegisterVehicle(VehicleModel vehicleModel)
        {
            using (HttpClient client = new HttpClient())
            {
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
                        //StringBuilder sbControlHtml = new StringBuilder();
                        //using (StringWriter stringWriter = new StringWriter())
                        //{
                        //    using (HtmlTextWriter htmlWriter = new HtmlTextWriter(stringWriter))
                        //    {
                        //        HtmlGenericControl formControl = new HtmlGenericControl("form");
                        //        formControl.Attributes.Add("Action", "GetVehicles");
                        //        formControl.Attributes.Add("Method", "Post");

                        //        HtmlGenericControl inputControl = new HtmlGenericControl("input");
                        //        inputControl.Attributes.Add("id", "checkbox");
                        //        inputControl.Attributes.Add("type", "checkbox");
                        //        inputControl.Attributes.Add("value", vehicle.RegistrationNumber);

                        //        formControl.Controls.Add(inputControl);

                        //        formControl.RenderControl(htmlWriter);
                        //        sbControlHtml.Append(stringWriter.ToString());
                        //        formControl.Dispose();
                                
                        //    }
                        //}

                        string vehicleAsText = $"{vehicle.RegistrationNumber} | {vehicle.Model} | {vehicle.Brand}";
                        listAsText += $"{vehicleAsText}\n";
                    }
                    ViewBag.Content = listAsText;
                }
            }
            return View("GetVehicleList");
        }
    }
}
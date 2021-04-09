using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;
using VehicleRegister.Client.Models.Account;
using VehicleRegister.DTO.dto;
using System.Configuration;

namespace VehicleRegister.Client.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult LoginView()
        {
            return View("Login");
        }

        public ActionResult CreateAccountView()
        {
            return View("CreateAccount");
        }

        public ActionResult CreateAccount(AccountModel AccountModel)
        {
            var url = ConfigurationManager.AppSettings["hostname"];
            using (HttpClient client = new HttpClient())
            {
                AccountRequestDto request = AccountModel.GetDto();
                var jsonRequestString = JsonConvert.SerializeObject(request);
                var stringContent = new StringContent(jsonRequestString, UnicodeEncoding.UTF8, "application/json");
                var response = client.PostAsync(url + "api/createaccount", stringContent).Result;
            }
            ViewBag.message = "Account has been registered!";
            return View("CreateAccount");
        }

        //public ActionResult Login(LoginModel loginModel)
        //{ 
        
        //}
        
    }
}
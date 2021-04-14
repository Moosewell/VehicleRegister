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
using VehicleRegister.Client.Models;

namespace VehicleRegister.Client.Controllers
{
    public class AccountController : Controller
    {
        private readonly EndPoints endPoints;

        public AccountController()
        {
            endPoints = new EndPoints();
        }

        public ActionResult LoginView()
        {
            return View("Login");
        }

        public ActionResult CreateAccountView()
        {
            return View("CreateAccount");
        }

        private string GetToken(LoginModel loginModel)
        {
            string token = "";
            using (HttpClient client = new HttpClient())
            {
                //var jsonRequestString = JsonConvert.SerializeObject(loginModel.GetDto());
                var stringContent = new StringContent(loginModel.GetTokenRequestString(), Encoding.UTF8, "application/x-www-form-urlencoded");
                var response = client.PostAsync(endPoints.GetToken, stringContent).Result;
                if(response != null)
                {
                    token = response.Content.ReadAsStringAsync().Result;
                    //var jsonString = response.Content.ReadAsStringAsync().Result;
                    //token = JsonConvert.DeserializeObject<string>(jsonString);
                }
            }
            return token;
        }

        public ActionResult Login(LoginModel loginModel)
        {
            string token = GetToken(loginModel);
            if(string.IsNullOrEmpty(token))
            {
                ViewBag.message = "Incorrect Username and Password!";
                return View("Login");
            }

            var userIdentity = new UserIdentity
            {
                Username = loginModel.Username,
                Token = token
            };

            var loggedInUsers = new List<UserIdentity>();
            loggedInUsers.Add(userIdentity);
            Session["OurBearerToken"] = loggedInUsers;

            return RedirectToAction("HomeView", "VehicleRegister");
            //return View("~/Views/VehicleRegister/Home");
        }

        public ActionResult CreateAccount(AccountModel AccountModel)
        {
           
            using (HttpClient client = new HttpClient())
            {
                AccountRequestDto request = AccountModel.GetDto();
                var jsonRequestString = JsonConvert.SerializeObject(request);
                var stringContent = new StringContent(jsonRequestString, UnicodeEncoding.UTF8, "application/json");
                var response = client.PostAsync(endPoints.CreateAccount, stringContent).Result;
            }
            ViewBag.message = "Account has been registered!";
            return View("CreateAccount");
        }
        
    }
}
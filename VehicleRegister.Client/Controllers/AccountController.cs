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

        private TokenReciever GetToken(LoginModel loginModel)
        {
            TokenReciever RecievedToken = null;
            using (HttpClient client = new HttpClient())
            {
                var stringContent = new StringContent(loginModel.GetTokenRequestString(), Encoding.UTF8, "application/x-www-form-urlencoded");
                var response = client.PostAsync(endPoints.GetToken, stringContent).Result;
                if(response != null)
                {
                    var jsonString = response.Content.ReadAsStringAsync().Result;
                    RecievedToken = JsonConvert.DeserializeObject<TokenReciever>(jsonString);
                }
            }
            return RecievedToken;
        }

        public ActionResult Login(LoginModel loginModel)
        {
            TokenReciever token = GetToken(loginModel);
            Session["TokenKey"] = token.token_type + " " + token.access_token;

            return RedirectToAction("HomeView", "VehicleRegister");
        }

        public ActionResult Logout()
        {
            Session["TokenKey"] = null;
            return View("Login");
        }

        public ActionResult CreateAccount(AccountModel AccountModel)
        {
           
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", Session["TokenKey"].ToString());
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
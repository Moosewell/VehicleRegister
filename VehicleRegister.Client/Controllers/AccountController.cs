using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using VehicleRegister.Client.Models.Account;

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
            //using (HttpClient client = new HttpClient())
            //{
            //    
            //}
            ViewBag.message = "Account has been registered!";
            return View("CreateAccount");
        }

        //public ActionResult Login(LoginModel loginModel)
        //{ 
        
        //}
        
    }
}
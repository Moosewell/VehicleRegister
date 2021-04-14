using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using VehicleRegister.DTO.dto;

namespace VehicleRegister.Client.Models.Account
{
    public class LoginModel
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public string GrantType { get; set; }

        public LoginRequestDto GetDto()
        {
            LoginRequestDto request = new LoginRequestDto();
            request.Username = Username;
            var passwordByte = Encoding.UTF8.GetBytes(Password);
            request.Password = Convert.ToBase64String(passwordByte);
            GrantType = "password";

            return request;
        }

        public string GetTokenRequestString()
        {
            var passwordByte = Encoding.UTF8.GetBytes(Password);
            var encryptedPassword = Convert.ToBase64String(passwordByte);
            string request = $"username={Username}&password={encryptedPassword}&grant_type=password";

            return request;
        }
    }
}
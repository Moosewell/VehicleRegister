using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using VehicleRegister.DTO.dto;

namespace VehicleRegister.Client.Models.Account
{
    public class AccountModel
    {
        public string Username { get; set; }
        public string Authorization { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        public AccountRequestDto GetDto()
        {
            if (Password != ConfirmPassword)
                throw new Exception("Password not confirmed");

            AccountRequestDto request = new AccountRequestDto();
            request.Username = Username;
            request.Authorization = Authorization;
            var passwordByte = Encoding.UTF8.GetBytes(Password);
            request.Password = Convert.ToBase64String(passwordByte);

            return request;
        }

        
    }
}
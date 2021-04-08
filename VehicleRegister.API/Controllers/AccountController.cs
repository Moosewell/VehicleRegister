using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using VehicleRegister.Domain.Classes;
using VehicleRegister.DTO.dto;
using VehicleRegister.Repository.Interfaces;

namespace VehicleRegister.API.Controllers
{
    public class AccountController : ApiController
    {
        private readonly IAccountRepository accountRepository;
        public AccountController(IAccountRepository accountRepository)
        {
            this.accountRepository = accountRepository;
        }

        [HttpPost]
        [Route("CreateAccount")]
        public IHttpActionResult CreateAccount(AccountRequestDto accountRequestDto)
        {
            var account = new Account(accountRequestDto.Username, accountRequestDto.Authorization, accountRequestDto.Password);
            accountRepository.RegisterAccount(account);
            return Ok();
        }
    }
}
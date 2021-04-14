using System.Web.Http;
using VehicleRegister.Domain.Classes;
using VehicleRegister.DTO.dto;
using VehicleRegister.Providers;
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

        [Authorize(Roles = "Super Admin")]
        [HttpPost]
        [Route("api/createaccount")]
        public IHttpActionResult CreateAccount(AccountRequestDto accountRequestDto)
        {
            var account = new Account(accountRequestDto.Username, accountRequestDto.Authorization, accountRequestDto.Password);
            accountRepository.RegisterAccount(account);
            return Ok();
        }
    }
}
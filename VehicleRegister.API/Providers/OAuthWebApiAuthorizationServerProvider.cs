using Microsoft.Owin.Security.OAuth;
using System;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using VehicleRegister.Repository.Classes;
using VehicleRegister.Repository.Interfaces;

namespace VehicleRegister.Providers
{
    public class OAuthWebApiAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        private readonly IAccountRepository accountRepository;

        public OAuthWebApiAuthorizationServerProvider()
        {
            accountRepository = new AzureRepository();
        }
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            //If the cridentials is ok, generate the token for passing through the headers for other access.
            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            var accountList = accountRepository.GetAllAccounts();
            var account = accountList.Where(o => o.Username == context.UserName).FirstOrDefault();
            if (account == null)
            {
                context.SetError("Username doesn't exist in database");
                return;
            }

            var databasePasswordByte = Convert.FromBase64String(account.Password);
            var databasePassword = Encoding.UTF8.GetString(databasePasswordByte);

            var inputPasswordByte = Convert.FromBase64String(context.Password);
            var inputPassword = Encoding.UTF8.GetString(inputPasswordByte);
            

            if(inputPassword == databasePassword)
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, account.Authorization));
                identity.AddClaim(new Claim(ClaimTypes.Name, account.Username));
                context.Validated(identity);
            }
            else
            {
                context.SetError("Invalid grant", "provided username and password is incorrect.");
                return;
            }
        }
    }
}
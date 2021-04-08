using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleRegister.Domain.Interfaces;

namespace VehicleRegister.Repository.Interfaces
{
    public interface IAccountRepository
    {
        void RegisterAccount(IAccount account);
        List<IAccount> GetAllAccounts();
        IAccount GetAccount(string username);
        void UpdateAccount(IAccount account);
        void DeleteAccount(string username);
    }
}

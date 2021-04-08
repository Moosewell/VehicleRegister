using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleRegister.Domain.Interfaces
{
    public interface IAccount
    {
        string Username { get;}
        string Authorization { get;}
        string Password { get;}
    }
}

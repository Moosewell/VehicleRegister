using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleRegister.Domain.Interfaces
{
    public interface IService
    {
        DateTime Date { get; }
        string Description { get; }

    }
}

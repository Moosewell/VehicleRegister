using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleRegister.DTO.dto
{
    public class AccountRequestDto
    {
        public string Username { get; set; }
        public string Authorization { get; set; }
        public string Password { get; set; }
    }
}

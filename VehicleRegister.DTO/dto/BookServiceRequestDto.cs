using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleRegister.DTO.dto
{
    public class BookServiceRequestDto
    {
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public List<string> vehicleRegistrationNumbers { get; set; } = new List<string>();
    }
}

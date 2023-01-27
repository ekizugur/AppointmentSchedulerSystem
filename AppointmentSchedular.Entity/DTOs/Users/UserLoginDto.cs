using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentSchedular.Entity.DTOs.Users
{
    public class UserLoginDto
    {
      
        public string EMail { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}

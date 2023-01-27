using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentSchedular.Entity.DTOs.Appointments
{
    public class AppointmentAddDto
    {
        public string Name { get; set; }
        public DateTimeOffset AppointmentDate { get; set; }
        public Guid UserId { get; set; }
        public string CreatedBy { get; set; }
    }
}

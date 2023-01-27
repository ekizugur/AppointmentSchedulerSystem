using AppointmentSchedular.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentSchedular.Entity.Entities
{
    public class Appointment:EntityBase
    {
        public string Name { get; set; }
        public Guid UserId { get; set; }
        public AppUser User { get; set; }
        [Required]
        public DateTimeOffset AppointmentDate { get; set; }

    }
}

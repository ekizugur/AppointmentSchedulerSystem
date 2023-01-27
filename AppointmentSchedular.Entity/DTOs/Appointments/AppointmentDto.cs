﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentSchedular.Entity.DTOs.Appointments
{
    public class AppointmentDto
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTimeOffset AppointmentDate { get; set; }
        public Guid userId { get; set; }
        public string Name { get; set; }
        public string CreatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public string Email { get; set; }
    }
}

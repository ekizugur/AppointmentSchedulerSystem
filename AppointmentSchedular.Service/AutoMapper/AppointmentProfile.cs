using AppointmentSchedular.Entity.DTOs.Appointments;
using AppointmentSchedular.Entity.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentSchedular.Service.AutoMapper
{
    public class AppointmentProfile:Profile
    {
        public AppointmentProfile()
        {
            CreateMap<AppointmentDto,Appointment>().ReverseMap();            
            CreateMap<AppointmentUpdateDto,AppointmentDto>().ReverseMap();
            CreateMap<AppointmentUpdateDto, Appointment>().ReverseMap();
        }
    }
}

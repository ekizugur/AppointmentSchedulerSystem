using AppointmentSchedular.Entity.DTOs.Users;
using AppointmentSchedular.Entity.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentSchedular.Service.AutoMapper.Users
{
    public class UserProfile:Profile
    {
        public UserProfile() {
            
            CreateMap<UserDto,AppUser>().ReverseMap();
            //CreateMap<UserRegisterDto,AppUser>().ReverseMap();
        }
    }
}

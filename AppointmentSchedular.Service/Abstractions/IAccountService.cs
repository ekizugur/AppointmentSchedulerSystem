using AppointmentSchedular.Entity.DTOs.Users;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentSchedular.Service.Abstractions
{
    public interface IAccountService
    {
        Task<IdentityResult> ChangePasswordAsync(ResetPasswordDto resetPasswordDto);
    }
}

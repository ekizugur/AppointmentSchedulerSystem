using AppointmentSchedular.Data.Context;
using AppointmentSchedular.Entity.DTOs.Users;
using AppointmentSchedular.Entity.Entities;
using AppointmentSchedular.Service.Abstractions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentSchedular.Service.Concretes
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly IUserService userService;
        private readonly IConfiguration config;
        private readonly AppDbContext dbContext;

        public AccountService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IUserService userService, IConfiguration config, AppDbContext dbContext)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.userService = userService;
            this.config = config;
            this.dbContext = dbContext;
        }
        public async Task<IdentityResult> ChangePasswordAsync(ResetPasswordDto resetPasswordDto)
        {
            var userId = userService.GetUserId();
            var user = await userManager.FindByIdAsync(userId);
            return await userManager.ChangePasswordAsync(user, resetPasswordDto.CurrentPassword, resetPasswordDto.NewPassword);

        }
    }
}

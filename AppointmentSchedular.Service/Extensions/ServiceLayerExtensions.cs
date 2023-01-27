using AppointmentSchedular.Data.Context;
using AppointmentSchedular.Data.Repositories.Abstractions;
using AppointmentSchedular.Data.Repositories.Concretes;
using AppointmentSchedular.Data.UnitOfWorks;
using AppointmentSchedular.Entity.Entities;
using AppointmentSchedular.Service.Abstractions;
using AppointmentSchedular.Service.Concretes;
using AppointmentSchedular.Service.Jobs;
using AppointmentSchedular.Service.Jobs.Abstracts;
using AppointmentSchedular.Service.Jobs.Concretes;
using FluentAssertions.Common;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace AppointmentSchedular.Service.Extensions
{
    public static class ServiceLayerExtensions
    {
        public static IServiceCollection LoadServiceLayerExtension(this IServiceCollection services, IConfiguration config)
        {
           
            var assembly=Assembly.GetExecutingAssembly();
            services.AddScoped<IAppointmentService, AppointmentService>();
            services.AddScoped<IMailService, MailService>();
            services.AddScoped<IRememberMailJob, RememberMailJob>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddAutoMapper(assembly);
            services.AddHangfire(configuration=>configuration
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UseSqlServerStorage(config.GetConnectionString("HangfireConnection")
            , new SqlServerStorageOptions
            {
                CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                QueuePollInterval = TimeSpan.Zero,
                UseRecommendedIsolationLevel = true,
                DisableGlobalLocks = true
            }));
            
            services.AddHangfireServer();
            return services;
        }
    }
}

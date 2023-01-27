using AppointmentSchedular.Data.Context;
using AppointmentSchedular.Entity.DTOs.Appointments;
using AppointmentSchedular.Entity.DTOs.Users;
using AppointmentSchedular.MVC.Models;
using AppointmentSchedular.Service.Abstractions;
using AppointmentSchedular.Service.Jobs;
using AppointmentSchedular.Service.Jobs.Abstracts;
using AppointmentSchedular.Service.Jobs.Concretes;
using AutoMapper;
using FluentAssertions;
using Hangfire;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;

namespace AppointmentSchedular.MVC.Controllers
{

    public class HomeController : Controller
    {

        private readonly IAppointmentService appointmentService;
        private readonly AppDbContext dbContext;
        private readonly IMapper mapper;
        private readonly IRememberMailJob rememberMailJob;
       

        public HomeController(IAppointmentService appointmentService, AppDbContext dbContext, IMapper mapper, IRememberMailJob rememberMailJob)
        {
            this.appointmentService = appointmentService;
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.rememberMailJob = rememberMailJob;
            
        }
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var email = this.User.FindFirstValue(ClaimTypes.Email);
            var idU = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userId = dbContext.Users.AsNoTracking().FirstOrDefault(x => x.Email == email);
            RecurringJob.AddOrUpdate("MailJob", () => rememberMailJob.Run(), Cron.Daily); //hangfire 
            RecurringJob.AddOrUpdate("AutoDelitionJob", () => appointmentService.AppointmentAutoDeletion(), Cron.Minutely); //hangfire 

            TempData["Name"] = userId.FirstName;
            if (idU != null)
            {
                var appointments = await appointmentService.GetAppointmentForLoginUser(Guid.Parse(idU));
                return View(appointments);
            }
            else
            {
                return View(null);
            }

        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> AddAppointment()
        {

            return View();
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddAppointment(AppointmentAddDto appointmentAddDto)
        {
            if (appointmentAddDto.AppointmentDate < DateTimeOffset.Now.AddHours(2))
            {
                TempData["appointmentaddMessage"] = "Incorrect appointment date. If you want to make a new appointment, you can make an appointment at the earliest 2 hours later.";
            }
            else if (string.IsNullOrEmpty(appointmentAddDto.Name))
            {
                TempData["appointmentaddMessage"] = "Appointment Description cannot be empty";
            }
            else
            {
                appointmentAddDto.UserId = Guid.Parse(this.User.FindFirstValue(ClaimTypes.NameIdentifier));
                appointmentAddDto.CreatedBy = this.User.FindFirstValue(ClaimTypes.Name);
                //appointmentAddDto.AppointmentDate = DateTime.UtcNow()
                await appointmentService.CreateAppointment(appointmentAddDto);
                await Task.Delay(2000);
                TempData["appointmentaddMessageSuccess"] = "Appointment created succesfully";
                RedirectToAction("Index", "Home", new { Area = "Home" });
            }
            return View();
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> UpdateAppointment(Guid appointmentId)
        {

            var appointment = await appointmentService.GetAppointmentAsync(appointmentId);
            var articleUpdateDto = mapper.Map<AppointmentUpdateDto>(appointment);

            return View(articleUpdateDto);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> UpdateAppointment(AppointmentUpdateDto appointmentUpdateDto)
        {
            if (appointmentUpdateDto.AppointmentDate < DateTimeOffset.Now.AddHours(2))
            {
                TempData["updateErrorMessage"] = "Incorrect appointment date. If you want to make a new appointment, you can make an appointment at the earliest 2 hours later.";
            }
            else if (string.IsNullOrEmpty(appointmentUpdateDto.Name))
            {
                TempData["updateErrorMessage"] = "Appointment Description cannot be empty";
            }
            else
            {
                await appointmentService.UpdateAppointmentAsync(appointmentUpdateDto);
                TempData["updateMessage"] = "Appointment updated succesfully";
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> DeleteAppointment(Guid appointmentId)
        {
            await appointmentService.SafeDeleteAppointmentAsync(appointmentId);
            return RedirectToAction("Index", "Home", new { Area = "Home" });
        }

       
       
    }
}
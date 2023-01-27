using AppointmentSchedular.Data.Context;
using AppointmentSchedular.Data.UnitOfWorks;
using AppointmentSchedular.Entity.DTOs.Appointments;
using AppointmentSchedular.Entity.Entities;
using AppointmentSchedular.Service.Abstractions;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Xml.Schema;

namespace AppointmentSchedular.Service.Concretes
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly AppDbContext dbContext;
        private readonly IMapper mapper;
        private readonly IMailService mailService;

        public AppointmentService(IUnitOfWork unitOfWork,AppDbContext dbContext,IMapper mapper,IMailService mailService)
        {
            this.unitOfWork = unitOfWork;
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.mailService = mailService;
        }
       
        public async Task CreateAppointment(AppointmentAddDto appointmentAddDto)
        {           

            var appointment = new Appointment
            {
                
            //AppointmentDate = DateTime.Now,
            UserId = appointmentAddDto.UserId,
                CreatedBy = appointmentAddDto.CreatedBy,
                Name = appointmentAddDto.Name,
                AppointmentDate=appointmentAddDto.AppointmentDate,
                
            };
            await unitOfWork.GetRepository<Appointment>().AddAsync(appointment);
            await unitOfWork.SaveAsync();
        }

        public async Task SafeDeleteAppointmentAsync(Guid appointmentId)
        {
            var appointment = await unitOfWork.GetRepository<Appointment>().GetByGuidAsync(appointmentId);
            appointment.IsDeleted= true;
            await unitOfWork.GetRepository<Appointment>().UpdateAsync(appointment);
            await unitOfWork.SaveAsync();          

        }

        public async Task<List<AppointmentDto>> GetAllAppointmentAsync()
        {
            var appointments = await unitOfWork.GetRepository<Appointment>().GetAllAsync(x=>!x.IsDeleted);
            var map = mapper.Map<List<AppointmentDto>>(appointments);
            return map;
        }

        public async Task<AppointmentDto> GetAppointmentAsync(Guid appointmentId)
        {
            var appointment=await unitOfWork.GetRepository<Appointment>().GetAsync(x=>x.Id==appointmentId && !x.IsDeleted);
            var map=mapper.Map<AppointmentDto>(appointment);
            return map;
        }

        public async Task UpdateAppointmentAsync(AppointmentUpdateDto appointmentUpdateDto)
        {
            var appointment = await unitOfWork.GetRepository<Appointment>().GetAsync(x => x.Id == appointmentUpdateDto.Id && !x.IsDeleted);
            mapper.Map<AppointmentUpdateDto, Appointment>(appointmentUpdateDto, appointment);
            
            await unitOfWork.GetRepository<Appointment>().UpdateAsync(appointment);
            
            await unitOfWork.SaveAsync();
        }

        public async Task<List<AppointmentDto>> GetAppointmentForLoginUser(Guid userId)
        {
            var appointments = await unitOfWork.GetRepository<Appointment>().GetAllAsync(x => !x.IsDeleted && x.UserId==userId);
            var map = mapper.Map<List<AppointmentDto>>(appointments);
            return map;
        }
        public async Task<IList<AppointmentDto>> AppointmentAutoDeletion()
        {
            var appointments = await unitOfWork.GetRepository<Appointment>().GetAllAsync(x => !x.IsDeleted);
            foreach (var appointment in appointments)
            {
                if(appointment.AppointmentDate.DateTime < DateTimeOffset.Now.DateTime)
                {
                    appointment.IsDeleted= true; 
                    
                }
            }
            var map=mapper.Map<List<AppointmentDto>>(appointments);
            await unitOfWork.SaveAsync();
            return map;
        }
    }

}

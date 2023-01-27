using AppointmentSchedular.Entity.DTOs.Appointments;
using AppointmentSchedular.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentSchedular.Service.Abstractions
{
    public interface IAppointmentService
    {
        Task<List<AppointmentDto>> GetAllAppointmentAsync();
        Task CreateAppointment(AppointmentAddDto appointmentAddDto);
        Task<AppointmentDto> GetAppointmentAsync(Guid appointmentId);
        Task UpdateAppointmentAsync(AppointmentUpdateDto appointmentUpdateDto);
        Task SafeDeleteAppointmentAsync(Guid appointmentId);
        Task<List<AppointmentDto>> GetAppointmentForLoginUser(Guid userId);
        Task<IList<AppointmentDto>> AppointmentAutoDeletion();


    }
}

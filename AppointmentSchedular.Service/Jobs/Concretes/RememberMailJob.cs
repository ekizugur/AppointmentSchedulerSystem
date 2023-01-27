using AppointmentSchedular.Service.Abstractions;
using AppointmentSchedular.Service.Jobs.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentSchedular.Service.Jobs.Concretes
{
    public class RememberMailJob : IRememberMailJob
    {
        private readonly IAppointmentService appointmentService;
        private readonly IMailService mailService;

        public RememberMailJob(IAppointmentService appointmentService,IMailService mailService)
        {
            this.appointmentService = appointmentService;
            this.mailService = mailService;
        }
        public void Run()
        {
            var appointments=appointmentService.GetAllAppointmentAsync().GetAwaiter().GetResult();
            foreach (var aps in appointments)
            {
                if(aps.AppointmentDate < DateTimeOffset.Now.AddDays(5) && aps.IsDeleted==false)
                {
                    mailService.SendMessageAsync(aps.CreatedBy, "Appointment Reminder", $"appointment reminder email. Date of your appointment. {aps.AppointmentDate.DateTime}");
                }
            }
            
            
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentSchedular.Service.Abstractions
{
    public interface IMailService
    {
        Task SendMessageAsync(string to,string subject,string body);
    }
}

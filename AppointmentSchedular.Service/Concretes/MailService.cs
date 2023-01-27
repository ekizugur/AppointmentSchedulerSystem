using AppointmentSchedular.Service.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentSchedular.Service.Concretes
{
    public class MailService : IMailService
    {
        private readonly IConfiguration config;

        public MailService(IConfiguration config)
        {
            this.config = config;
        }
        public async Task SendMessageAsync(string to, string subject, string body)
        {
            MailMessage mail = new();
            mail.To.Add(to);
            mail.Subject = subject;
            mail.Body = body;
            mail.From = new("ekizugur03@gmail.com","Appointment Service",System.Text.Encoding.UTF8);
            mail.IsBodyHtml= true;
            mail.BodyEncoding= Encoding.UTF8;

            SmtpClient smtp = new();
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(config.GetSection("EmailUserName").Value, config.GetSection("EmailPassword").Value);
            smtp.Port = 587;
            
            smtp.Host = config.GetSection("EmailHost").Value;
            
            smtp.EnableSsl = true;
            await smtp.SendMailAsync(mail);
        }
    }
}

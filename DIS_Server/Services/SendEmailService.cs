using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace DIS_Server.Services
{

    public interface ISendEmailService
    {
        Task SendConfirmLetter(string email, string name, string url);
    }

    public class SendEmailService : ISendEmailService
    {
        private readonly IConfigurationSection _configuration;

        public SendEmailService(IConfiguration configuration)
        {
            _configuration = configuration.GetSection("EmailConnection");  
        }

        public Task SendConfirmLetter(string email, string name, string url)
        {

            string subject = "Підтвердження пошти";
            string text = $"<p>Вітаємо, {name}</p> <p>Ви отримали цей лист тому, що " +
                           "ваша адреса була використана при реєстрації.</p>" +
                           "</p> Перейдіть за цим посиланням щоб підтвердити реєстрацію: " +
                          $"</p> <a href={url}>Підтвердити</a>";
            return SendLetters(new[] { email }, subject, text);

        }

        public async Task SendLetters(string[] email, string subject, string text, string fromName = "")
        {
            string senderEmail = _configuration["Email"];
            string senderPassword = _configuration["Password"];
            string smtpServer = _configuration["Smtp"];
            int port = Convert.ToInt32(_configuration["Port"]);
            bool ssl = Convert.ToBoolean(_configuration["SSL"]);
            if (string.IsNullOrEmpty(fromName))
                fromName = _configuration["DefaultName"];
            var message = new MailMessage
            {
                Body = text,
                Subject = subject,
                From = new MailAddress(senderEmail, fromName),
                IsBodyHtml = true
            };

            foreach (var e in email)
            {
                message.To.Add(e);
            }
            SmtpClient smtpClient = new SmtpClient() { Host = smtpServer, Port = port, EnableSsl = ssl };
            NetworkCredential networkCredential = new NetworkCredential(senderEmail, senderPassword);
            smtpClient.Credentials = networkCredential;
            await Task.Run(() => smtpClient.Send(message));
            Console.WriteLine("Send  letter to : " + email);
        }
    }
}

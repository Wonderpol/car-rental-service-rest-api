using System.IO;
using System.Threading.Tasks;
using CarRentalRestApi.Models.Mailing;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace CarRentalRestApi.Services.MailingService
{
    public class MailService : IMailService
    {
        private readonly MailSettings _mailSettings;

        public MailService(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }

        public async Task SendEmail(MailRequest mailRequest)
        {
            var filePath = Directory.GetCurrentDirectory() + "/Templates/RentConfirmationEmailTemplate.html";
            var str = new StreamReader(filePath);
            var mailText = await str.ReadToEndAsync();
            str.Close();
            mailText = mailText.Replace("{userName}", mailRequest.User.FirstName).Replace("{reservationId}", $"{mailRequest.Rent.Id}")
                .Replace("{vehicleBrand}", mailRequest.Vehicle.Brand + " " +mailRequest.Vehicle.Model).Replace("{vehicleRegNum}", mailRequest.Vehicle.RegistrationPlate);
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
            email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
            email.Subject = "Reservation confirmation";
            var builder = new BodyBuilder
            {
                HtmlBody = mailText
            };
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(_mailSettings.Mail, _mailSettings.Password);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }
    }
}
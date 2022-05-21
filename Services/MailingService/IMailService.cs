using System.Threading.Tasks;
using CarRentalRestApi.Models.Mailing;

namespace CarRentalRestApi.Services.MailingService
{
    public interface IMailService
    {
        Task SendEmail(MailRequest mailRequest);
    }
}
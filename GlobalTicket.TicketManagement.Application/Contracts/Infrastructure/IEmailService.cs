using GlobalTicket.TicketManagement.Application.Models.Mail;
using System.Threading.Tasks;

namespace GlobalTicket.TicketManagement.Application.Contracts.Infrastructure
{
    public interface IEmailService
    {
        Task<bool> SendEmail(Email email);
    }
}

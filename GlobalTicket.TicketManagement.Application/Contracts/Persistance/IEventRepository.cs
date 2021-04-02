using GlobalTicket.TicketManagement.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace GlobalTicket.TicketManagement.Application.Contracts.Persistance
{
    public interface IEventRepository : IAsyncRepository<Event>
    {
        Task<bool> IsEventNameAndDateUnique(string name, DateTime date);
    }
}

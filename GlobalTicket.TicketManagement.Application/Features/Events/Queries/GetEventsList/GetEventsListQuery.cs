using MediatR;
using System.Collections.Generic;

namespace GlobalTicket.TicketManagement.Application.Features.Events.Queries.GetEventsList
{
    public class GetEventsListQuery : IRequest<List<EventListViewModel>>
    {
    }
}

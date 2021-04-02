using AutoMapper;
using GlobalTicket.TicketManagement.Application.Contracts.Persistance;
using GlobalTicket.TicketManagement.Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GlobalTicket.TicketManagement.Application.Features.Events.Queries.GetEventsList
{
    public class GetEventsListQueryHandler : IRequestHandler<GetEventsListQuery, List<EventListViewModel>>
    {
        private readonly IAsyncRepository<Event> eventRepository;
        private readonly IMapper mapper;

        public GetEventsListQueryHandler(IAsyncRepository<Event> eventRepository, IMapper mapper)
        {
            this.eventRepository = eventRepository;
            this.mapper = mapper;
        }

        public async Task<List<EventListViewModel>> Handle(GetEventsListQuery request, CancellationToken cancellationToken)
        {
            var events = (await eventRepository.ListAllAsync()).OrderBy(_ => _.Date);
            return mapper.Map<List<EventListViewModel>>(events);
        }
    }
}

using AutoMapper;
using GlobalTicket.TicketManagement.Application.Contracts.Persistance;
using GlobalTicket.TicketManagement.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace GlobalTicket.TicketManagement.Application.Features.Events.Queries.GetEventDetail
{
    public class GetEventDetailQueryHandler : IRequestHandler<GetEventDetailQuery, EventDetailViewModel>
    {
        private readonly IAsyncRepository<Event> eventRepository;
        private readonly IAsyncRepository<Category> categoryRepository;
        private readonly IMapper mapper;

        public GetEventDetailQueryHandler(IAsyncRepository<Event> eventRepository, IAsyncRepository<Category> categoryRepository, IMapper mapper)
        {
            this.eventRepository = eventRepository;
            this.categoryRepository = categoryRepository;
            this.mapper = mapper;
        }

        public async Task<EventDetailViewModel> Handle(GetEventDetailQuery request, CancellationToken cancellationToken)
        {
            var @event = await eventRepository.GetByIdAsync(request.Id);
            var eventDetailsVM = mapper.Map<EventDetailViewModel>(@event);

            var category = await categoryRepository.GetByIdAsync(@event.CategoryId);
            eventDetailsVM.Category = mapper.Map<CategoryDto>(category);

            return eventDetailsVM;
        }
    }
}

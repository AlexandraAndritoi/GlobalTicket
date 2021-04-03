using AutoMapper;
using GlobalTicket.TicketManagement.Application.Contracts.Infrastructure;
using GlobalTicket.TicketManagement.Application.Contracts.Persistance;
using GlobalTicket.TicketManagement.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GlobalTicket.TicketManagement.Application.Features.Events.Queries.GetEventsExport
{
    public class GetEventsExportQueryHandler : IRequestHandler<GetEventsExportQuery, EventExportFileViewModel>
    {
        private readonly IAsyncRepository<Event> eventRepository;
        private readonly ICsvExporter csvExporter;
        private readonly IMapper mapper;

        public GetEventsExportQueryHandler(IAsyncRepository<Event> eventRepository, ICsvExporter csvExporter, IMapper mapper)
        {
            this.eventRepository = eventRepository;
            this.csvExporter = csvExporter;
            this.mapper = mapper;
        }

        public async Task<EventExportFileViewModel> Handle(GetEventsExportQuery request, CancellationToken cancellationToken)
        {
            var allEvents = mapper.Map<List<EventExportDto>>((await eventRepository.ListAllAsync()).OrderBy(x => x.Date));

            var fileData = csvExporter.ExportEventsToCsv(allEvents);

            var eventExportFileDto = new EventExportFileViewModel() { ContentType = "text/csv", Data = fileData, EventExportFileName = $"{Guid.NewGuid()}.csv" };

            return eventExportFileDto;
        }
    }
}

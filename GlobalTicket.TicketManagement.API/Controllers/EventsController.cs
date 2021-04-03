using GlobalTicket.TicketManagement.API.Utility;
using GlobalTicket.TicketManagement.Application.Features.Events.Commands.CreateEvent;
using GlobalTicket.TicketManagement.Application.Features.Events.Commands.DeleteEvent;
using GlobalTicket.TicketManagement.Application.Features.Events.Commands.UpdateEvent;
using GlobalTicket.TicketManagement.Application.Features.Events.Queries.GetEventDetail;
using GlobalTicket.TicketManagement.Application.Features.Events.Queries.GetEventsExport;
using GlobalTicket.TicketManagement.Application.Features.Events.Queries.GetEventsList;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GlobalTicket.TicketManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventsController : ControllerBase
    {
        private readonly IMediator mediator;

        public EventsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet(Name = "GetAllEvents")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<EventListViewModel>>> GetAllEvents()
        {
            var dtos = await mediator.Send(new GetEventsListQuery());
            return Ok(dtos);
        }

        [HttpGet("{id}", Name = "GetEventById")]
        public async Task<ActionResult<EventDetailViewModel>> GetEventById(Guid id)
        {
            var getEventDetailQuery = new GetEventDetailQuery() { Id = id };
            return Ok(await mediator.Send(getEventDetailQuery));
        }

        [HttpPost(Name = "AddEvent")]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateEventCommand createEventCommand)
        {
            var id = await mediator.Send(createEventCommand);
            return Ok(id);
        }

        [HttpPut(Name = "UpdateEvent")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Update([FromBody] UpdateEventCommand updateEventCommand)
        {
            await mediator.Send(updateEventCommand);
            return NoContent();
        }

        [HttpDelete("{id}", Name = "DeleteEvent")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Delete(Guid id)
        {
            var deleteEventCommand = new DeleteEventCommand() { EventId = id };
            await mediator.Send(deleteEventCommand);
            return NoContent();
        }

        [HttpGet("export", Name = "ExportEvents")]
        [FileResultContentType("text/csv")]
        public async Task<FileResult> ExportEvents()
        {
            var fileDto = await mediator.Send(new GetEventsExportQuery());

            return File(fileDto.Data, fileDto.ContentType, fileDto.EventExportFileName);
        }
    }
}

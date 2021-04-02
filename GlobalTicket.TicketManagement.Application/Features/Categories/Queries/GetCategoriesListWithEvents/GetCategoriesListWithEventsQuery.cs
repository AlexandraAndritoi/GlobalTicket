using MediatR;
using System.Collections.Generic;

namespace GlobalTicket.TicketManagement.Application.Features.Categories.Queries.GetCategoriesListWithEvents
{
    public class GetCategoriesListWithEventsQuery : IRequest<List<CategoryEventListViewModel>>
    {
        public bool IncludeHistory { get; set; }
    }
}

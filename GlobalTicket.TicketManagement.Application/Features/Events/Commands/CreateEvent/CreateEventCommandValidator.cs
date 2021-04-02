using FluentValidation;
using GlobalTicket.TicketManagement.Application.Contracts.Persistance;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GlobalTicket.TicketManagement.Application.Features.Events.Commands.CreateEvent
{
    public class CreateEventCommandValidator : AbstractValidator<CreateEventCommand>
    {
        private readonly IEventRepository eventRepository;

        public CreateEventCommandValidator(IEventRepository eventRepository)
        {
            this.eventRepository = eventRepository;
        }

        public CreateEventCommandValidator()
        {
            RuleFor(_ => _.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

            RuleFor(_ => _.Date)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .GreaterThan(DateTime.Now);

            RuleFor(_ => _.Price)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .GreaterThan(0);

            RuleFor(_ => _)
                .MustAsync(EventNameAndDateUnique)
                .WithMessage("An event with the same name and date already exists.");
        }

        private async Task<bool> EventNameAndDateUnique(CreateEventCommand e, CancellationToken token)
        {
            return !await eventRepository.IsEventNameAndDateUnique(e.Name, e.Date);
        }
    }
}

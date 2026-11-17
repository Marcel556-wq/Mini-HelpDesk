using MiniHelpDesk.Commands;
using MiniHelpDesk.Data;
using MiniHelpDesk.Validators;

namespace MiniHelpDesk.Handlers
{
    /*
    klasa: AddTicketCommandHandler
    opis:
    Obsługuje dodawanie nowych zgłoszeń po sprawdzeniu ich poprawności.
    pola / właściwości:
    repository, TicketRepository, repozytorium zgłoszeń
    validator, TicketValidator, walidator zgłoszeń
    */
    public class AddTicketCommandHandler :
        ICommandHandler<AddTicketCommand, CommandResult>
    {
        private readonly TicketRepository repository;
        private readonly TicketValidator validator;

        public AddTicketCommandHandler(
            TicketRepository repository,
            TicketValidator validator)
        {
            this.repository = repository;
            this.validator = validator;
        }

        /*
        funkcja: Handle
        opis:
        Sprawdza poprawność danych i dodaje zgłoszenie do repozytorium.
        parametry:
        command, AddTicketCommand, komenda dodania zgłoszenia
        zwraca:
        CommandResult, wynik operacji
        */
        public CommandResult Handle(AddTicketCommand command)
        {
            var errors = validator.Validate(command.Ticket);

            if (errors.Count > 0)
                return CommandResult.Fail(errors);

            repository.Add(command.Ticket);

            return CommandResult.Ok();
        }
    }
}
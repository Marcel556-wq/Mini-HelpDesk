using MiniHelpDesk.Commands;
using MiniHelpDesk.Data;
using MiniHelpDesk.Validators;

namespace MiniHelpDesk.Handlers
{
    /*
    klasa: UpdateTicketCommandHandler
    opis:
    Obsługuje edycję istniejących zgłoszeń po sprawdzeniu poprawności danych.
    pola / właściwości:
    repository, TicketRepository, repozytorium zgłoszeń
    validator, TicketValidator, walidator zgłoszeń
    */
    public class UpdateTicketCommandHandler :
        ICommandHandler<UpdateTicketCommand, CommandResult>
    {
        private readonly TicketRepository repository;
        private readonly TicketValidator validator;

        public UpdateTicketCommandHandler(
            TicketRepository repository,
            TicketValidator validator)
        {
            this.repository = repository;
            this.validator = validator;
        }

        /*
        funkcja: Handle
        opis:
        Sprawdza dane i aktualizuje istniejące zgłoszenie.
        parametry:
        command, UpdateTicketCommand, komenda edycji zgłoszenia
        zwraca:
        CommandResult, wynik operacji
        */
        public CommandResult Handle(UpdateTicketCommand command)
        {
            var errors = validator.Validate(command.Ticket);

            if (errors.Count > 0)
                return CommandResult.Fail(errors);

            bool updated = repository.Update(command.Ticket);

            if (!updated)
            {
                errors["General"] = "Nie znaleziono zgłoszenia.";
                return CommandResult.Fail(errors);
            }

            return CommandResult.Ok();
        }
    }
}
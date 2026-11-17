using MiniHelpDesk.Commands;
using MiniHelpDesk.Data;

namespace MiniHelpDesk.Handlers
{
    /*
    klasa: ChangeTicketStatusCommandHandler
    opis:
    Obsługuje zmianę statusu istniejącego zgłoszenia.
    pola / właściwości:
    repository, TicketRepository, repozytorium zgłoszeń
    */
    public class ChangeTicketStatusCommandHandler :
        ICommandHandler<ChangeTicketStatusCommand, bool>
    {
        private readonly TicketRepository repository;

        private static readonly string[] AllowedStatuses =
        {
            "Nowe",
            "W trakcie",
            "Zakończone"
        };

        public ChangeTicketStatusCommandHandler(
            TicketRepository repository)
        {
            this.repository = repository;
        }

        /*
        funkcja: Handle
        opis:
        Sprawdza status i przypisuje go do wskazanego zgłoszenia.
        parametry:
        command, ChangeTicketStatusCommand, komenda zmiany statusu
        zwraca:
        bool, informacja czy status został zmieniony
        */
        public bool Handle(ChangeTicketStatusCommand command)
        {
            if (!AllowedStatuses.Contains(command.Status))
                return false;

            var ticket = repository.GetById(command.TicketId);

            if (ticket == null)
                return false;

            ticket.Status = command.Status;

            return true;
        }
    }
}
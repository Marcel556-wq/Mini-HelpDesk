using MiniHelpDesk.Commands;
using MiniHelpDesk.Data;

namespace MiniHelpDesk.Handlers
{
    /*
    klasa: DeleteTicketCommandHandler
    opis:
    Obsługuje usuwanie zgłoszeń z repozytorium.
    pola / właściwości:
    repository, TicketRepository, repozytorium zgłoszeń
    */
    public class DeleteTicketCommandHandler :
        ICommandHandler<DeleteTicketCommand, bool>
    {
        private readonly TicketRepository repository;

        public DeleteTicketCommandHandler(TicketRepository repository)
        {
            this.repository = repository;
        }

        /*
        funkcja: Handle
        opis:
        Wyszukuje zgłoszenie i usuwa je z repozytorium.
        parametry:
        command, DeleteTicketCommand, komenda usunięcia
        zwraca:
        bool, informacja czy zgłoszenie zostało usunięte
        */
        public bool Handle(DeleteTicketCommand command)
        {
            var ticket = repository.GetById(command.TicketId);

            if (ticket == null)
                return false;

            return repository.Remove(ticket);
        }
    }
}
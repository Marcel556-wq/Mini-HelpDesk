using MiniHelpDesk.Data;
using MiniHelpDesk.Models;

namespace MiniHelpDesk.Queries
{
    /*
    klasa: GetTicketByIdQueryHandler
    opis:
    Obsługuje wyszukiwanie zgłoszenia po jego identyfikatorze.
    pola / właściwości:
    repository, TicketRepository, repozytorium zgłoszeń
    */
    public class GetTicketByIdQueryHandler :
        IQueryHandler<GetTicketByIdQuery, Ticket?>
    {
        private readonly TicketRepository repository;

        public GetTicketByIdQueryHandler(TicketRepository repository)
        {
            this.repository = repository;
        }

        /*
        funkcja: Handle
        opis:
        Wyszukuje zgłoszenie o podanym identyfikatorze.
        parametry:
        query, GetTicketByIdQuery, zapytanie o konkretne zgłoszenie
        zwraca:
        Ticket?, znalezione zgłoszenie lub null
        */
        public Ticket? Handle(GetTicketByIdQuery query)
        {
            return repository.GetById(query.TicketId);
        }
    }
}
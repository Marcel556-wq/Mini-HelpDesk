using MiniHelpDesk.Data;
using MiniHelpDesk.Models;

namespace MiniHelpDesk.Queries
{
    /*
    klasa: GetTicketsQueryHandler
    opis:
    Obsługuje pobieranie wszystkich zgłoszeń z repozytorium.
    pola / właściwości:
    repository, TicketRepository, repozytorium zgłoszeń
    */
    public class GetTicketsQueryHandler :
        IQueryHandler<GetTicketsQuery, IReadOnlyList<Ticket>>
    {
        private readonly TicketRepository repository;

        public GetTicketsQueryHandler(TicketRepository repository)
        {
            this.repository = repository;
        }

        /*
        funkcja: Handle
        opis:
        Zwraca listę wszystkich zgłoszeń.
        parametry:
        query, GetTicketsQuery, zapytanie o wszystkie zgłoszenia
        zwraca:
        IReadOnlyList<Ticket>, lista zgłoszeń
        */
        public IReadOnlyList<Ticket> Handle(GetTicketsQuery query)
        {
            return repository.GetAll();
        }
    }
}
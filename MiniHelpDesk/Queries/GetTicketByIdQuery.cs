namespace MiniHelpDesk.Queries
{
    /*
    klasa: GetTicketByIdQuery
    opis:
    Przechowuje identyfikator zgłoszenia przeznaczonego do wyszukania.
    pola / właściwości:
    TicketId, int, identyfikator zgłoszenia
    */
    public class GetTicketByIdQuery
    {
        public int TicketId { get; }

        public GetTicketByIdQuery(int ticketId)
        {
            TicketId = ticketId;
        }
    }
}
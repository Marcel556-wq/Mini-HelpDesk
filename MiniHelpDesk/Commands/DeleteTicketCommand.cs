namespace MiniHelpDesk.Commands
{
    /*
    klasa: DeleteTicketCommand
    opis:
    Przechowuje identyfikator zgłoszenia przeznaczonego do usunięcia.
    pola / właściwości:
    TicketId, int, identyfikator zgłoszenia
    */
    public class DeleteTicketCommand
    {
        public int TicketId { get; }

        public DeleteTicketCommand(int ticketId)
        {
            TicketId = ticketId;
        }
    }
}
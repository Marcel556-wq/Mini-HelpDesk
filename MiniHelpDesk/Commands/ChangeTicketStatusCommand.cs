namespace MiniHelpDesk.Commands
{
    /*
    klasa: ChangeTicketStatusCommand
    opis:
    Przechowuje identyfikator zgłoszenia i nowy status.
    pola / właściwości:
    TicketId, int, identyfikator zgłoszenia
    Status, string, nowy status zgłoszenia
    */
    public class ChangeTicketStatusCommand
    {
        public int TicketId { get; }
        public string Status { get; }

        public ChangeTicketStatusCommand(int ticketId, string status)
        {
            TicketId = ticketId;
            Status = status;
        }
    }
}
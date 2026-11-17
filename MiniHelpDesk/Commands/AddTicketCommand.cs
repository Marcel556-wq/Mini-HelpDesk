using MiniHelpDesk.Models;

namespace MiniHelpDesk.Commands
{
    /*
    klasa: AddTicketCommand
    opis:
    Przechowuje dane zgłoszenia, które ma zostać dodane.
    pola / właściwości:
    Ticket, Ticket, zgłoszenie przeznaczone do dodania
    */
    public class AddTicketCommand
    {
        public Ticket Ticket { get; }

        public AddTicketCommand(Ticket ticket)
        {
            Ticket = ticket;
        }
    }
}
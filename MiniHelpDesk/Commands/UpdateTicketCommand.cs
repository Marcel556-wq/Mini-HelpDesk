using MiniHelpDesk.Models;

namespace MiniHelpDesk.Commands
{
    /*
    klasa: UpdateTicketCommand
    opis:
    Przechowuje nowe dane zgłoszenia przeznaczonego do edycji.
    pola / właściwości:
    Ticket, Ticket, zgłoszenie z aktualizowanymi danymi
    */
    public class UpdateTicketCommand
    {
        public Ticket Ticket { get; }

        public UpdateTicketCommand(Ticket ticket)
        {
            Ticket = ticket;
        }
    }
}

using System.Collections.ObjectModel;
using MiniHelpDesk.Models;

namespace MiniHelpDesk.Data
{
    /*
    klasa: TicketRepository

    opis:
    Przechowuje zgłoszenia techniczne w pamięci
    i udostępnia operacje na kolekcji zgłoszeń.

    pola / właściwości:
    Tickets, ObservableCollection<Ticket>, kolekcja zgłoszeń
    nextId, int, kolejny unikalny identyfikator
    */

    public class TicketRepository
    {
        public ObservableCollection<Ticket> Tickets { get; } = new();

        private int nextId = 1;

        /*
        funkcja: Add

        opis:
        Dodaje nowe zgłoszenie do kolekcji i nadaje mu unikalne ID.

        parametry:
        ticket, Ticket, zgłoszenie do dodania

        zwraca:
        void, nic nie zwraca
        */
        public void Add(Ticket ticket)
        {
            ticket.Id = nextId++;
            ticket.Name = ticket.Name.Trim();
            ticket.Description = ticket.Description.Trim();

            Tickets.Add(ticket);
        }

        /*
        funkcja: Remove

        opis:
        Usuwa wskazane zgłoszenie z kolekcji.

        parametry:
        ticket, Ticket, zgłoszenie przeznaczone do usunięcia

        zwraca:
        bool, informacja czy zgłoszenie zostało usunięte
        */
        public bool Remove(Ticket ticket)
        {
            return Tickets.Remove(ticket);
        }

        /*
        funkcja: GetAll

        opis:
        Zwraca wszystkie zgłoszenia przechowywane w repozytorium.

        parametry:
        brak

        zwraca:
        IReadOnlyList<Ticket>, lista wszystkich zgłoszeń
        */
        public IReadOnlyList<Ticket> GetAll()
        {
            return Tickets;
        }

        /*
        funkcja: GetById

        opis:
        Wyszukuje zgłoszenie na podstawie jego identyfikatora.

        parametry:
        id, int, identyfikator szukanego zgłoszenia

        zwraca:
        Ticket?, znalezione zgłoszenie lub null
        */
        public Ticket? GetById(int id)
        {
            return Tickets.FirstOrDefault(ticket => ticket.Id == id);
        }

        /*
        funkcja: Update

        opis:
        Aktualizuje nazwę, opis i priorytet istniejącego zgłoszenia.

        parametry:
        ticket, Ticket, zgłoszenie z nowymi danymi

        zwraca:
        bool, informacja czy zgłoszenie zostało zaktualizowane
        */
        public bool Update(Ticket ticket)
        {
            Ticket? existing = GetById(ticket.Id);

            if (existing == null)
                return false;

            existing.Name = ticket.Name.Trim();
            existing.Description = ticket.Description.Trim();
            existing.Priority = ticket.Priority;

            return true;
        }
    }
}

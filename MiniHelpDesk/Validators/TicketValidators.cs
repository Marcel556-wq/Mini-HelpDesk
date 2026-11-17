
using MiniHelpDesk.Models;

namespace MiniHelpDesk.Validators
{
    /*
    klasa: TicketValidator

    opis:
    Sprawdza poprawność danych zgłoszenia technicznego
    przed dodaniem lub edycją zgłoszenia.

    pola / właściwości:
    brak
    */

    public class TicketValidator
    {
        private static readonly string[] AllowedPriorities =
        {
            "Niski",
            "Średni",
            "Wysoki"
        };

        /*
        funkcja: Validate

        opis:
        Sprawdza nazwę, opis i priorytet zgłoszenia.

        parametry:
        ticket, Ticket, zgłoszenie przeznaczone do walidacji

        zwraca:
        Dictionary<string, string>, lista błędów przypisanych do pól
        */
        public Dictionary<string, string> Validate(Ticket ticket)
        {
            var errors = new Dictionary<string, string>();

            string name = ticket.Name?.Trim() ?? "";
            string description = ticket.Description?.Trim() ?? "";
            string priority = ticket.Priority?.Trim() ?? "";

            if (name.Length < 3)
            {
                errors["Name"] =
                    "Nazwa musi zawierać minimum 3 znaki.";
            }

            if (description.Length < 10)
            {
                errors["Description"] =
                    "Opis musi zawierać minimum 10 znaków.";
            }

            if (!AllowedPriorities.Contains(priority))
            {
                errors["Priority"] =
                    "Wybierz priorytet: Niski, Średni lub Wysoki.";
            }

            return errors;
        }

        /*
        funkcja: IsValid

        opis:
        Sprawdza, czy zgłoszenie nie zawiera błędów walidacji.

        parametry:
        ticket, Ticket, sprawdzane zgłoszenie

        zwraca:
        bool, true jeśli dane są poprawne
        */
        public bool IsValid(Ticket ticket)
        {
            return Validate(ticket).Count == 0;
        }
    }
}

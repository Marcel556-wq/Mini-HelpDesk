
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MiniHelpDesk.Models
{
    /*
    klasa: Ticket

    opis:
    Przechowuje dane pojedynczego zgłoszenia technicznego
    i informuje interfejs o zmianach właściwości.

    pola / właściwości:
    Id, int, unikalny identyfikator zgłoszenia
    Name, string, nazwa zgłoszenia
    Description, string, opis problemu
    Priority, string, priorytet zgłoszenia
    Status, string, aktualny status zgłoszenia
    */

    public class Ticket : INotifyPropertyChanged
    {
        public int Id { get; set; }

        private string name = "";

        public string Name
        {
            get => name;
            set
            {
                if (name != value)
                {
                    name = value;
                    OnPropertyChanged();
                }
            }
        }

        private string description = "";

        public string Description
        {
            get => description;
            set
            {
                if (description != value)
                {
                    description = value;
                    OnPropertyChanged();
                }
            }
        }

        private string priority = "Niski";

        public string Priority
        {
            get => priority;
            set
            {
                if (priority != value)
                {
                    priority = value;
                    OnPropertyChanged();
                }
            }
        }

        private string status = "Nowe";

        public string Status
        {
            get => status;
            set
            {
                if (status != value)
                {
                    status = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        /*
        funkcja: OnPropertyChanged

        opis:
        Powiadamia interfejs o zmianie właściwości zgłoszenia.

        parametry:
        propertyName, string, nazwa zmienionej właściwości

        zwraca:
        void, nic nie zwraca
        */
        protected void OnPropertyChanged(
            [CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(
                this,
                new PropertyChangedEventArgs(propertyName));
        }
    }
}

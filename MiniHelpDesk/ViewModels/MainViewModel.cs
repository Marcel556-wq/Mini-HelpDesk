using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using MiniHelpDesk.Commands;
using MiniHelpDesk.Models;
using MiniHelpDesk.Queries;

namespace MiniHelpDesk.ViewModels
{
    /*
    klasa: MainViewModel
    opis:
    Zarządza danymi i operacjami głównego ekranu aplikacji Mini HelpDesk.
    pola / właściwości:
    Tickets, ObservableCollection<Ticket>, kolekcja zgłoszeń
    SelectedTicket, Ticket?, aktualnie wybrane zgłoszenie
    TicketName, string, nazwa zgłoszenia
    TicketDescription, string, opis zgłoszenia
    TicketPriority, string, priorytet zgłoszenia
    TotalTickets, int, liczba wszystkich zgłoszeń
    OpenTickets, int, liczba niezakończonych zgłoszeń
    */
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly CommandBus commandBus;
        private readonly QueryBus queryBus;

        public ObservableCollection<Ticket> Tickets { get; }

        private Ticket? selectedTicket;
        public Ticket? SelectedTicket
        {
            get => selectedTicket;
            set
            {
                selectedTicket = value;
                OnPropertyChanged();
                RefreshCommands();
            }
        }

        private string ticketName = "";
        public string TicketName
        {
            get => ticketName;
            set
            {
                ticketName = value;
                OnPropertyChanged();
            }
        }

        private string ticketDescription = "";
        public string TicketDescription
        {
            get => ticketDescription;
            set
            {
                ticketDescription = value;
                OnPropertyChanged();
            }
        }

        private string ticketPriority = "Niski";
        public string TicketPriority
        {
            get => ticketPriority;
            set
            {
                ticketPriority = value;
                OnPropertyChanged();
            }
        }

        private string nameError = "";
        public string NameError
        {
            get => nameError;
            set
            {
                nameError = value;
                OnPropertyChanged();
            }
        }

        private string descriptionError = "";
        public string DescriptionError
        {
            get => descriptionError;
            set
            {
                descriptionError = value;
                OnPropertyChanged();
            }
        }

        private string priorityError = "";
        public string PriorityError
        {
            get => priorityError;
            set
            {
                priorityError = value;
                OnPropertyChanged();
            }
        }

        private string formMessage = "";
        public string FormMessage
        {
            get => formMessage;
            set
            {
                formMessage = value;
                OnPropertyChanged();
            }
        }

        private string newStatus = "W trakcie";
        public string NewStatus
        {
            get => newStatus;
            set
            {
                newStatus = value;
                OnPropertyChanged();
            }
        }

        private bool isEditing;
        public bool IsEditing
        {
            get => isEditing;
            set
            {
                isEditing = value;
                OnPropertyChanged();
                RefreshCommands();
            }
        }

        private int totalTickets;
        public int TotalTickets
        {
            get => totalTickets;
            set
            {
                totalTickets = value;
                OnPropertyChanged();
            }
        }

        private int openTickets;
        public int OpenTickets
        {
            get => openTickets;
            set
            {
                openTickets = value;
                OnPropertyChanged();
            }
        }

        public ICommand AddCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand StartEditCommand { get; }
        public ICommand SaveEditCommand { get; }
        public ICommand CancelEditCommand { get; }
        public ICommand ChangeStatusCommand { get; }

        /*
        funkcja: MainViewModel
        opis:
        Inicjalizuje kolekcję zgłoszeń i polecenia dostępne na ekranie.
        parametry:
        commandBus, CommandBus, obsługa komend
        queryBus, QueryBus, obsługa zapytań
        zwraca:
        brak, konstruktor klasy
        */
        public MainViewModel(
            CommandBus commandBus,
            QueryBus queryBus)
        {
            this.commandBus = commandBus;
            this.queryBus = queryBus;

            var existingTickets =
                queryBus.Send<GetTicketsQuery, IReadOnlyList<Ticket>>(
                    new GetTicketsQuery());

            Tickets = new ObservableCollection<Ticket>(existingTickets);

            AddCommand = new RelayCommand(AddTicket, () => !IsEditing);
            DeleteCommand = new RelayCommand(
                DeleteTicket,
                () => SelectedTicket != null && !IsEditing);

            StartEditCommand = new RelayCommand(
                StartEditing,
                () => SelectedTicket != null && !IsEditing);

            SaveEditCommand = new RelayCommand(
                SaveEditing,
                () => IsEditing);

            CancelEditCommand = new RelayCommand(
                CancelEditing,
                () => IsEditing);

            ChangeStatusCommand = new RelayCommand(
                ChangeStatus,
                () => SelectedTicket != null && !IsEditing);

            UpdateCounters();
        }

        /*
        funkcja: AddTicket
        opis:
        Sprawdza dane i dodaje nowe zgłoszenie.
        parametry: brak
        zwraca: void, nic nie zwraca
        */
        private void AddTicket()
        {
            var ticket = new Ticket
            {
                Name = TicketName,
                Description = TicketDescription,
                Priority = TicketPriority
            };

            var result = commandBus.Send<AddTicketCommand, CommandResult>(
                new AddTicketCommand(ticket));

            if (!result.Success)
            {
                ShowErrors(result.Errors);
                return;
            }

            Tickets.Add(ticket);

            ClearForm();
            UpdateCounters();

            FormMessage = "Zgłoszenie zostało dodane.";
        }

        /*
        funkcja: DeleteTicket
        opis:
        Usuwa aktualnie wybrane zgłoszenie.
        parametry: brak
        zwraca: void, nic nie zwraca
        */
        private void DeleteTicket()
        {
            if (SelectedTicket == null)
                return;

            var ticket = SelectedTicket;

            bool deleted = commandBus.Send<DeleteTicketCommand, bool>(
                new DeleteTicketCommand(ticket.Id));

            if (!deleted)
            {
                FormMessage = "Nie udało się usunąć zgłoszenia.";
                return;
            }

            Tickets.Remove(ticket);
            SelectedTicket = null;

            UpdateCounters();

            FormMessage = "Zgłoszenie zostało usunięte.";
        }

        /*
        funkcja: StartEditing
        opis:
        Przenosi dane wybranego zgłoszenia do formularza edycji.
        parametry: brak
        zwraca: void, nic nie zwraca
        */
        private void StartEditing()
        {
            if (SelectedTicket == null)
                return;

            TicketName = SelectedTicket.Name;
            TicketDescription = SelectedTicket.Description;
            TicketPriority = SelectedTicket.Priority;

            ClearErrors();

            IsEditing = true;
            FormMessage = "Edytujesz zgłoszenie.";
        }

        /*
        funkcja: SaveEditing
        opis:
        Zapisuje zmiany w wybranym zgłoszeniu.
        parametry: brak
        zwraca: void, nic nie zwraca
        */
        private void SaveEditing()
        {
            if (SelectedTicket == null)
                return;

            var editedTicket = new Ticket
            {
                Id = SelectedTicket.Id,
                Name = TicketName,
                Description = TicketDescription,
                Priority = TicketPriority,
                Status = SelectedTicket.Status
            };

            var result = commandBus.Send<UpdateTicketCommand, CommandResult>(
                new UpdateTicketCommand(editedTicket));

            if (!result.Success)
            {
                ShowErrors(result.Errors);
                return;
            }

            SelectedTicket.Name = editedTicket.Name.Trim();
            SelectedTicket.Description = editedTicket.Description.Trim();
            SelectedTicket.Priority = editedTicket.Priority;

            IsEditing = false;
            ClearForm();
            UpdateCounters();

            FormMessage = "Zmiany zostały zapisane.";
        }

        /*
        funkcja: CancelEditing
        opis:
        Anuluje edycję i czyści formularz.
        parametry: brak
        zwraca: void, nic nie zwraca
        */
        private void CancelEditing()
        {
            IsEditing = false;
            ClearForm();

            FormMessage = "Edycja została anulowana.";
        }

        /*
        funkcja: ChangeStatus
        opis:
        Zmienia status aktualnie wybranego zgłoszenia.
        parametry: brak
        zwraca: void, nic nie zwraca
        */
        private void ChangeStatus()
        {
            if (SelectedTicket == null)
                return;

            bool changed = commandBus.Send<ChangeTicketStatusCommand, bool>(
                new ChangeTicketStatusCommand(
                    SelectedTicket.Id,
                    NewStatus));

            if (!changed)
            {
                FormMessage = "Nie udało się zmienić statusu.";
                return;
            }

            UpdateCounters();

            FormMessage = "Status zgłoszenia został zmieniony.";
        }

        /*
        funkcja: ShowErrors
        opis:
        Wyświetla błędy walidacji formularza.
        parametry:
        errors, Dictionary<string,string>, lista błędów
        zwraca:
        void, nic nie zwraca
        */
        private void ShowErrors(Dictionary<string, string> errors)
        {
            ClearErrors();

            if (errors.TryGetValue("Name", out string? name))
                NameError = name;

            if (errors.TryGetValue("Description", out string? description))
                DescriptionError = description;

            if (errors.TryGetValue("Priority", out string? priority))
                PriorityError = priority;

            if (errors.TryGetValue("General", out string? general))
                FormMessage = general;
        }

        /*
        funkcja: ClearForm
        opis:
        Czyści pola formularza i komunikaty błędów.
        parametry: brak
        zwraca:
        void, nic nie zwraca
        */
        private void ClearForm()
        {
            TicketName = "";
            TicketDescription = "";
            TicketPriority = "Niski";

            ClearErrors();
        }

        /*
        funkcja: ClearErrors
        opis:
        Usuwa komunikaty błędów formularza.
        parametry: brak
        zwraca:
        void, nic nie zwraca
        */
        private void ClearErrors()
        {
            NameError = "";
            DescriptionError = "";
            PriorityError = "";
        }

        /*
        funkcja: UpdateCounters
        opis:
        Aktualizuje liczbę wszystkich i niezakończonych zgłoszeń.
        parametry: brak
        zwraca:
        void, nic nie zwraca
        */
        private void UpdateCounters()
        {
            TotalTickets = Tickets.Count;

            OpenTickets = Tickets.Count(
                ticket => ticket.Status != "Zakończone");
        }

        /*
        funkcja: RefreshCommands
        opis:
        Odświeża warunki wykonywania poleceń interfejsu.
        parametry: brak
        zwraca:
        void, nic nie zwraca
        */
        private void RefreshCommands()
        {
            (AddCommand as RelayCommand)?.RaiseCanExecuteChanged();
            (DeleteCommand as RelayCommand)?.RaiseCanExecuteChanged();
            (StartEditCommand as RelayCommand)?.RaiseCanExecuteChanged();
            (SaveEditCommand as RelayCommand)?.RaiseCanExecuteChanged();
            (CancelEditCommand as RelayCommand)?.RaiseCanExecuteChanged();
            (ChangeStatusCommand as RelayCommand)?.RaiseCanExecuteChanged();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        /*
        funkcja: OnPropertyChanged
        opis:
        Informuje interfejs o zmianie właściwości ViewModelu.
        parametry:
        propertyName, string?, nazwa zmienionej właściwości
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
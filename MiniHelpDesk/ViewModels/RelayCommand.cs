using System.Windows.Input;

namespace MiniHelpDesk.ViewModels
{
    /*
    klasa: RelayCommand
    opis:
    Obsługuje wykonywanie poleceń z poziomu interfejsu użytkownika.
    pola / właściwości:
    execute, Action, metoda wykonywana przez polecenie
    canExecute, Func<bool>?, warunek możliwości wykonania polecenia
    */
    public class RelayCommand : ICommand
    {
        private readonly Action execute;
        private readonly Func<bool>? canExecute;

        public RelayCommand(Action execute, Func<bool>? canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        /*
        funkcja: CanExecute
        opis:
        Sprawdza, czy polecenie może zostać wykonane.
        parametry:
        parameter, object?, parametr polecenia
        zwraca:
        bool, informacja czy można wykonać polecenie
        */
        public bool CanExecute(object? parameter)
        {
            return canExecute == null || canExecute();
        }

        /*
        funkcja: Execute
        opis:
        Wykonuje przypisaną metodę.
        parametry:
        parameter, object?, parametr polecenia
        zwraca:
        void, nic nie zwraca
        */
        public void Execute(object? parameter)
        {
            execute();
        }

        public event EventHandler? CanExecuteChanged;

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
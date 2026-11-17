namespace MiniHelpDesk.Commands
{
    /*
    klasa: CommandResult
    opis:
    Przechowuje informację o powodzeniu komendy oraz ewentualne błędy.
    pola / właściwości:
    Success, bool, informacja czy operacja się powiodła
    Errors, Dictionary<string,string>, lista błędów
    */
    public class CommandResult
    {
        public bool Success { get; }
        public Dictionary<string, string> Errors { get; }

        private CommandResult(bool success, Dictionary<string, string>? errors = null)
        {
            Success = success;
            Errors = errors ?? new Dictionary<string, string>();
        }

        /*
        funkcja: Ok
        opis:
        Tworzy wynik informujący o poprawnym wykonaniu komendy.
        parametry: brak
        zwraca: CommandResult, wynik pozytywny
        */
        public static CommandResult Ok()
        {
            return new CommandResult(true);
        }

        /*
        funkcja: Fail
        opis:
        Tworzy wynik informujący o błędach komendy.
        parametry:
        errors, Dictionary<string,string>, lista błędów
        zwraca: CommandResult, wynik negatywny
        */
        public static CommandResult Fail(Dictionary<string, string> errors)
        {
            return new CommandResult(false, errors);
        }
    }
}

namespace MiniHelpDesk.Commands
{
    /*
    klasa: ICommandHandler

    opis:
    Definiuje wspólny interfejs dla handlerów obsługujących komendy.

    pola / właściwości:
    brak
    */

    public interface ICommandHandler<TCommand, TResult>
    {
        /*
        funkcja: Handle

        opis:
        Wykonuje operację określoną przez otrzymaną komendę.

        parametry:
        command, TCommand, komenda do wykonania

        zwraca:
        TResult, wynik wykonanej operacji
        */
        TResult Handle(TCommand command);
    }
}

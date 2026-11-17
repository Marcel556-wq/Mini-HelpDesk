namespace MiniHelpDesk.Queries
{
    /*
    klasa: IQueryHandler
    opis:
    Definiuje wspólny interfejs dla handlerów obsługujących zapytania.
    pola / właściwości:
    brak
    */
    public interface IQueryHandler<TQuery, TResult>
    {
        /*
        funkcja: Handle
        opis:
        Wykonuje zapytanie i zwraca jego wynik.
        parametry:
        query, TQuery, zapytanie do wykonania
        zwraca:
        TResult, wynik zapytania
        */
        TResult Handle(TQuery query);
    }
}
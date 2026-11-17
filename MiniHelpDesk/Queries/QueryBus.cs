using Microsoft.Extensions.DependencyInjection;

namespace MiniHelpDesk.Queries
{
    /*
    klasa: QueryBus
    opis:
    Przekazuje zapytania do odpowiednich handlerów za pomocą Dependency Injection.
    pola / właściwości:
    serviceProvider, IServiceProvider, dostawca handlerów zapytań
    */
    public class QueryBus
    {
        private readonly IServiceProvider serviceProvider;

        public QueryBus(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        /*
        funkcja: Send
        opis:
        Wyszukuje odpowiedni handler i przekazuje mu zapytanie.
        parametry:
        query, TQuery, zapytanie do wykonania
        zwraca:
        TResult, wynik zapytania
        */
        public TResult Send<TQuery, TResult>(TQuery query)
        {
            var handler = serviceProvider
                .GetRequiredService<IQueryHandler<TQuery, TResult>>();

            return handler.Handle(query);
        }
    }
}
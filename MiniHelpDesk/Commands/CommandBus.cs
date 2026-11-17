
using Microsoft.Extensions.DependencyInjection;

namespace MiniHelpDesk.Commands
{
    /*
    klasa: CommandBus

    opis:
    Przekazuje komendy do odpowiednich handlerów
    za pomocą systemu Dependency Injection.

    pola / właściwości:
    serviceProvider, IServiceProvider, dostawca handlerów
    */

    public class CommandBus
    {
        private readonly IServiceProvider serviceProvider;

        public CommandBus(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        /*
        funkcja: Send

        opis:
        Wyszukuje odpowiedni handler i przekazuje mu komendę.

        parametry:
        command, TCommand, komenda do wykonania

        zwraca:
        TResult, wynik działania handlera
        */
        public TResult Send<TCommand, TResult>(TCommand command)
        {
            var handler = serviceProvider
                .GetRequiredService<ICommandHandler<TCommand, TResult>>();

            return handler.Handle(command);
        }
    }
}

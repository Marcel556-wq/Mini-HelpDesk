using Microsoft.Extensions.Logging;
using MiniHelpDesk.Commands;
using MiniHelpDesk.Data;
using MiniHelpDesk.Handlers;
using MiniHelpDesk.Models;
using MiniHelpDesk.Queries;
using MiniHelpDesk.Validators;
using MiniHelpDesk.ViewModels;

namespace MiniHelpDesk
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();

            builder
                .UseMauiApp<App>();

            // Repozytorium i walidator
            builder.Services.AddSingleton<TicketRepository>();
            builder.Services.AddSingleton<TicketValidator>();

            // CommandBus
            builder.Services.AddSingleton<CommandBus>();

            // Handlery komend
            builder.Services.AddTransient<
                ICommandHandler<AddTicketCommand, CommandResult>,
                AddTicketCommandHandler>();

            builder.Services.AddTransient<
                ICommandHandler<DeleteTicketCommand, bool>,
                DeleteTicketCommandHandler>();

            builder.Services.AddTransient<
                ICommandHandler<UpdateTicketCommand, CommandResult>,
                UpdateTicketCommandHandler>();

            builder.Services.AddTransient<
                ICommandHandler<ChangeTicketStatusCommand, bool>,
                ChangeTicketStatusCommandHandler>();

            // QueryBus
            builder.Services.AddSingleton<QueryBus>();

            // Handlery zapytań
            builder.Services.AddTransient<
                IQueryHandler<GetTicketsQuery, IReadOnlyList<Ticket>>,
                GetTicketsQueryHandler>();

            builder.Services.AddTransient<
                IQueryHandler<GetTicketByIdQuery, Ticket?>,
                GetTicketByIdQueryHandler>();

            // ViewModel i strona główna
            builder.Services.AddTransient<MainViewModel>();
            builder.Services.AddTransient<MainPage>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
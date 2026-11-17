using Microsoft.Extensions.DependencyInjection;

namespace MiniHelpDesk
{
    public partial class App : Application
    {
        public App(IServiceProvider serviceProvider)
        {
            InitializeComponent();

            MainPage = serviceProvider.GetRequiredService<MainPage>();
        }
    }
}
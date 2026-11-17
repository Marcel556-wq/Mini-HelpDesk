using MiniHelpDesk.ViewModels;

namespace MiniHelpDesk
{
    /*
    klasa: MainPage
    opis:
    Reprezentuje główny ekran aplikacji Mini HelpDesk.
    pola / właściwości:
    brak
    */
    public partial class MainPage : ContentPage
    {
        /*
        funkcja: MainPage
        opis:
        Inicjalizuje ekran i przypisuje mu główny ViewModel.
        parametry:
        viewModel, MainViewModel, logika głównego ekranu
        zwraca:
        brak, konstruktor klasy
        */
        public MainPage(MainViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}
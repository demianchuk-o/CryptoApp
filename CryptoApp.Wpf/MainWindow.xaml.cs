using System.Windows;
using CryptoApp.Wpf.Shared.Navigation;
using CryptoApp.Wpf.Shared.Navigation.Manager;
using Microsoft.Extensions.DependencyInjection;

namespace CryptoApp.Wpf;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private readonly IFrameNavigationManager _frameNavigationManager;
    public MainWindow()
    {
        InitializeComponent();
        _frameNavigationManager = App.Current.ServiceProvider
            .GetRequiredService<IFrameNavigationManager>();
        
        Loaded += OnLoaded;
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        _frameNavigationManager.InitializeFrame(MainFrame, FrameType.Main);
        _frameNavigationManager.InitializeFrame(SearchFrame, FrameType.Search);
        
        _frameNavigationManager.GetNavigationService(FrameType.Main)
            .NavigateTo("TopCurrencies/TopCurrenciesPage");
        _frameNavigationManager.GetNavigationService(FrameType.Search)
            .NavigateTo("SearchCurrencies/SearchCurrenciesPage");
    }
}
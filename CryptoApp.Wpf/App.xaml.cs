using System.Configuration;
using System.Data;
using System.Net.Http;
using System.Windows;
using CryptoApp.Application.Crypto;
using CryptoApp.Infrastructure.API.CoinCap;
using CryptoApp.Wpf.SearchCurrencies;
using CryptoApp.Wpf.TopCurrencies;
using Microsoft.Extensions.DependencyInjection;

namespace CryptoApp.Wpf;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : System.Windows.Application
{
    public IServiceProvider ServiceProvider { get; }
    
    public App()
    {
        var services = new ServiceCollection();
        ConfigureServices(services);
        ServiceProvider = services.BuildServiceProvider();
    }
    public new static App Current => (App)System.Windows.Application.Current;
    private void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<HttpClient>();
        services.AddSingleton<ICoinCapApiClient, CoinCapApiClient>();

        services.AddTransient<ICryptoService, CryptoService>();
        services.AddTransient<TopCurrenciesPage>();
        services.AddTransient<TopCurrenciesViewModel>();
        services.AddTransient<SearchCurrenciesViewModel>();
        services.AddSingleton<MainWindow>();
    }
    
    private void OnStartup(object sender, StartupEventArgs startupEventArgs)
    {
        var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
        //var topCurrenciesPage = ServiceProvider.GetRequiredService<TopCurrenciesPage>();
        //mainWindow.MainFrame.Content = topCurrenciesPage;
        mainWindow.Show();
    }
}
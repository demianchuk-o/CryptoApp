using System.Configuration;
using System.Data;
using System.Net.Http;
using System.Windows;
using CryptoApp.Application.Crypto;
using CryptoApp.Infrastructure.API.CoinCap;
using CryptoApp.Wpf.TopCurrencies;
using Microsoft.Extensions.DependencyInjection;

namespace CryptoApp.Wpf;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : System.Windows.Application
{
    private readonly IServiceProvider _serviceProvider;

    public App()
    {
        var services = new ServiceCollection();

        services.AddSingleton<HttpClient>();
        services.AddSingleton<ICoinCapApiClient, CoinCapApiClient>();

        services.AddTransient<ICryptoService, CryptoService>();
        services.AddTransient<TopCurrenciesViewModel>();

        services.AddSingleton<MainWindow>();
        
        _serviceProvider = services.BuildServiceProvider();
    }
    
    protected override void OnStartup(StartupEventArgs e)
    {
        var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
        mainWindow.Show();
    }
}
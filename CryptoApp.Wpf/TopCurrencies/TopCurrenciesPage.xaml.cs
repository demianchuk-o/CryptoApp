using System.Windows;
using System.Windows.Controls;
using Microsoft.Extensions.DependencyInjection;

namespace CryptoApp.Wpf.TopCurrencies;

public partial class TopCurrenciesPage : Page
{
    private readonly TopCurrenciesViewModel _viewModel;

    public TopCurrenciesPage()
    {
        InitializeComponent();
        _viewModel = App.Current.ServiceProvider.GetRequiredService<TopCurrenciesViewModel>();
        DataContext = _viewModel;
    }
    
    private async void Page_Loaded(object sender, RoutedEventArgs e)
    {
        await _viewModel.LoadDataAsync();
    }
}
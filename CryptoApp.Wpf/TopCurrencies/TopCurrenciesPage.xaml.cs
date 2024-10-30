using System.Windows;
using System.Windows.Controls;
using Microsoft.Extensions.DependencyInjection;

namespace CryptoApp.Wpf.TopCurrencies;

public partial class TopCurrenciesPage : Page
{
    private TopCurrenciesViewModel _viewModel;
    public TopCurrenciesPage()
    {
        InitializeComponent();
        _viewModel = App.Current.ServiceProvider.GetRequiredService<TopCurrenciesViewModel>();
        DataContext = _viewModel;
    }

    protected override async void OnInitialized(EventArgs e)
    {
        base.OnInitialized(e);
        await _viewModel.LoadDataAsync();
    }
}
using System.Windows.Controls;
using Microsoft.Extensions.DependencyInjection;

namespace CryptoApp.Wpf.SearchCurrencies;

public partial class SearchCurrenciesPage : Page
{
    private SearchCurrenciesViewModel _viewModel;
    public SearchCurrenciesPage()
    {
        InitializeComponent();
        _viewModel = App.Current.ServiceProvider
            .GetRequiredService<SearchCurrenciesViewModel>();
        DataContext = _viewModel;
    }
}
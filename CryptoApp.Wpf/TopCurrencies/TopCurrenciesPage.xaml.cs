using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CryptoApp.Wpf.Shared.Navigation;
using CryptoApp.Wpf.Shared.Navigation.Manager;
using Microsoft.Extensions.DependencyInjection;

namespace CryptoApp.Wpf.TopCurrencies;

public partial class TopCurrenciesPage : Page
{
    private readonly TopCurrenciesViewModel _viewModel;
    private readonly INavigationService _navigationService;
    public TopCurrenciesPage()
    {
        InitializeComponent();
        _viewModel = App.Current.ServiceProvider.GetRequiredService<TopCurrenciesViewModel>();
        _navigationService = App.Current.ServiceProvider
            .GetRequiredService<IFrameNavigationManager>()
            .GetNavigationService(FrameType.Main);
        DataContext = _viewModel;
    }

    private void UIElement_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        _navigationService.NavigateToDetails("bitcoin");
    }
}
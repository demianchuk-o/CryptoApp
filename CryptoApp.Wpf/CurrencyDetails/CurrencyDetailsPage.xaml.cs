using System.Windows;
using System.Windows.Controls;
using CryptoApp.Wpf.Shared.Navigation;
using Microsoft.Extensions.DependencyInjection;

namespace CryptoApp.Wpf.CurrencyDetails;

public partial class CurrencyDetailsPage : Page
{
    private readonly CurrencyDetailsViewModel _viewModel;
    private string _id;
    private FrameType _frameType;
    public CurrencyDetailsPage()
    {
        InitializeComponent();
        _viewModel = App.Current.ServiceProvider
            .GetRequiredService<CurrencyDetailsViewModel>();
        DataContext = _viewModel;
        Loaded += OnLoaded;
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        FetchIdFromQuery();
        _viewModel.BaseId = _id;
        _viewModel.Initialize(_frameType);
    }

    private void FetchIdFromQuery()
    {
        if (NavigationService is null) return;
        string uri = NavigationService.CurrentSource.OriginalString;
        if (string.IsNullOrEmpty(uri)) return;

        string query = uri.Split('?')[1];
        if (string.IsNullOrEmpty(query)) return;

        query.TrimStart('?');

        var parameters = query.Split('&')
            .Select(p => p.Split('='))
            .ToDictionary(p => p[0], p => p[1]);

        if (!parameters.TryGetValue("id", out string? id)) return;
        if (!parameters.TryGetValue("frameType", out string? frameType)) return;
        _id = id;
        _frameType = Enum.Parse<FrameType>(frameType);
    }
}

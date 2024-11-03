using System.Windows.Controls;
using System.Windows.Navigation;

namespace CryptoApp.Wpf.Shared.Navigation;

public class NavigationService : INavigationService
{
    private Frame _frame;
    public void Initialize(Frame frame)
    {
        _frame = frame;
        _frame.NavigationUIVisibility = NavigationUIVisibility.Hidden;
    }

    public void NavigateTo(string pageUri, object parameter = null)
    {
        if(_frame is null)
            throw new InvalidOperationException("Frame is not initialized");

        _frame.Navigate(new Uri(pageUri, UriKind.Relative), parameter);
    }
    public void NavigateToDetails(string id)
    {
        if(_frame is null)
            throw new InvalidOperationException("Frame is not initialized");

        _frame.Navigate(new Uri($"CurrencyDetails/CurrencyDetailsPage.xaml?id={id}", UriKind.Relative));
    }
    public void GoBack()
    {
        if(_frame is null)
            throw new InvalidOperationException("Frame is not initialized");

        if(_frame.CanGoBack)
            _frame.GoBack();
    }

    public bool CanGoBack => _frame?.CanGoBack ?? false;
}
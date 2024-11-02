using System.Windows.Controls;

namespace CryptoApp.Wpf.Shared.Navigation;

public interface INavigationService
{
    void Initialize(Frame frame);
    void NavigateTo(string pageUri, object parameter = null);
    void GoBack();
    bool CanGoBack { get; }
}
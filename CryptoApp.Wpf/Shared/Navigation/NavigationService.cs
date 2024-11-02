using System.Windows.Controls;

namespace CryptoApp.Wpf.Shared.Navigation;

public class NavigationService : INavigationService
{
    private Frame _frame;
    public void Initialize(Frame frame)
    {
        throw new NotImplementedException();
    }

    public void NavigateTo(string pageUri, object parameter = null)
    {
        throw new NotImplementedException();
    }

    public void GoBack()
    {
        throw new NotImplementedException();
    }

    public bool CanGoBack { get; }
}
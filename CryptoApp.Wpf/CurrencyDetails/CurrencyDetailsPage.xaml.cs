using System.Windows;
using System.Windows.Controls;

namespace CryptoApp.Wpf.CurrencyDetails;

public partial class CurrencyDetailsPage : Page
{
    private string _id;
    public CurrencyDetailsPage()
    {
        InitializeComponent();
        Loaded += OnLoaded;
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
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
        _id = id;
        
        TextBox idTextBox = new()
        {
            Text = id
        };
        MainStackPanel.Children.Add(idTextBox);
    }   
}
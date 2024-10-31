using System.Windows;
using System.Windows.Controls;
using CryptoApp.Core.CryptoCurrencies;

namespace CryptoApp.Wpf.Shared.UI;

public partial class CryptoSearchItem : UserControl
{
    public static DependencyProperty CryptoCurrencyProperty { get; } 
        = DependencyProperty.Register(
            nameof(CryptoCurrency), typeof(CryptoCurrency), typeof(CryptoSearchItem), 
            new PropertyMetadata(default(CryptoCurrency)));

    public CryptoCurrency CryptoCurrency
    {
        get => (CryptoCurrency)GetValue(CryptoCurrencyProperty);
        set => SetValue(CryptoCurrencyProperty, value);
    }
    public CryptoSearchItem()
    {
        InitializeComponent();
        DataContext = this;
    }
}
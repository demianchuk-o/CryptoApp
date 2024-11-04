using System.Windows;
using System.Windows.Controls;
using CryptoApp.Core.CryptoCurrencies;

namespace CryptoApp.Wpf.Shared.UI;

public partial class CurrencyItem : UserControl
{
    public static DependencyProperty CryptoCurrencyProperty { get; }
        = DependencyProperty.Register(
            nameof(CryptoCurrency), typeof(CryptoCurrency), typeof(CurrencyItem),
            new PropertyMetadata(default(CryptoCurrency)));

    public CryptoCurrency CryptoCurrency
    {
        get => (CryptoCurrency)GetValue(CryptoCurrencyProperty);
        set => SetValue(CryptoCurrencyProperty, value);
    }
    public CurrencyItem()
    {
        InitializeComponent();
        DataContext = this;
    }
}
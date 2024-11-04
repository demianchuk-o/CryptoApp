using System.Windows;
using System.Windows.Controls;
using CryptoApp.Core.CryptoCurrencies;

namespace CryptoApp.Wpf.Shared.UI;

public partial class MarketItem : UserControl
{
    public static DependencyProperty MarketPriceProperty { get; }
        = DependencyProperty.Register(
            nameof(MarketPrice), typeof(MarketPrice), typeof(MarketItem),
            new PropertyMetadata(default(MarketPrice)));

    public MarketPrice MarketPrice
    {
        get => (MarketPrice)GetValue(MarketPriceProperty);
        set => SetValue(MarketPriceProperty, value);
    }
    public MarketItem()
    {
        InitializeComponent();
    }
}
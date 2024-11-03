using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CryptoApp.Core.CryptoCurrencies;

namespace CryptoApp.Wpf.Shared.UI;

public partial class CurrencyList : UserControl
{
    public static readonly DependencyProperty ItemsSourceProperty =
        DependencyProperty.Register(
            nameof(ItemsSource),
            typeof(ObservableCollection<CryptoCurrency>),
            typeof(CurrencyList),
            new PropertyMetadata(default(ObservableCollection<CryptoCurrency>)));

    public ObservableCollection<CryptoCurrency> ItemsSource
    {
        get => (ObservableCollection<CryptoCurrency>)GetValue(ItemsSourceProperty);
        set => SetValue(ItemsSourceProperty, value);
    }

    public static readonly DependencyProperty SelectCommandProperty =
        DependencyProperty.Register(
            nameof(SelectionCommand),
            typeof(ICommand),
            typeof(CurrencyList),
            new PropertyMetadata(default(ICommand)));

    public ICommand SelectionCommand
    {
        get => (ICommand)GetValue(SelectCommandProperty);
        set => SetValue(SelectCommandProperty, value);
    }
    public CurrencyList()
    {
        InitializeComponent();
    }
}
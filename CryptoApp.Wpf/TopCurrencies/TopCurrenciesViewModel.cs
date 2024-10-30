using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using CryptoApp.Application.Crypto;
using CryptoApp.Core.CryptoCurrencies;

namespace CryptoApp.Wpf.TopCurrencies;

public class TopCurrenciesViewModel : INotifyPropertyChanged
{
    private readonly ICryptoService _cryptoService;
    public ObservableCollection<CryptoCurrency> CryptoCurrencies { get; } = [];
    private int _limit = 10;
    
    public int Limit
    {
        get => _limit;
        set
        {
            if (_limit != value)
            {
                _limit = value;
                OnPropertyChanged(nameof(Limit));
            }
        }
    }
    
    public TopCurrenciesViewModel(ICryptoService cryptoService)
    {
        _cryptoService = cryptoService;
    }
    
    public async Task LoadDataAsync()
    {
        var result = await _cryptoService.GetTopCryptoCurrenciesAsync(Limit);
        if (result.IsSuccess)
        {
            CryptoCurrencies.Clear();
            foreach (var cryptoCurrency in result.Data)
            {
                CryptoCurrencies.Add(cryptoCurrency);
            }
        }
        else
        {
            MessageBox.Show(result.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
    
    public event PropertyChangedEventHandler? PropertyChanged;
    public void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
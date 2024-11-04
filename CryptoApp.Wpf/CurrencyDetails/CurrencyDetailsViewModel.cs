using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using CryptoApp.Application.Crypto;
using CryptoApp.Core.CryptoCurrencies;
using CryptoApp.Wpf.Shared;

namespace CryptoApp.Wpf.CurrencyDetails;

public class CurrencyDetailsViewModel : INotifyPropertyChanged
{
    private readonly ICryptoService _cryptoService;

    public CurrencyDetailsViewModel(ICryptoService cryptoService)
    {
        _cryptoService = cryptoService;
    }
    
    private string _id = string.Empty;
    public string Id
    {
        get => _id;
        set
        {
            if (_id == value) return;
            _id = value;
            OnPropertyChanged();
            _ = LoadPriceHistoryAsync();
        }
    }
    
    private AppState _priceHistoryState = AppState.Loading();
    public AppState PriceHistoryState
    {
        get => _priceHistoryState;
        private set
        {
            if (_priceHistoryState == value) return;
            _priceHistoryState = value;
            OnPropertyChanged();
        }
    }
    private string _errorMessage = string.Empty;
    public string ErrorMessage
    {
        get => _errorMessage;
        private set
        {
            if (_errorMessage == value) return;
            _errorMessage = value;
            OnPropertyChanged();
        }
    }
    public ObservableCollection<PriceDataPoint> PriceHistory { get; } = [];
    private async Task LoadPriceHistoryAsync()
    {
        PriceHistoryState = AppState.Loading();
        //date in unix timestamp format
        string firstDate = DateTimeOffset.UtcNow.AddDays(-1).ToUnixTimeMilliseconds().ToString();
        string secondDate = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString();
        var historyAsync = await _cryptoService.GetCryptoCurrencyHistoryAsync(Id, "m1", 
            firstDate, secondDate);
        if (historyAsync.IsSuccess)
        {
            PriceHistory.Clear();
            Console.WriteLine(historyAsync.Data.Data.Length);
            foreach (var priceDataPoint in historyAsync.Data.Data)
            {
                PriceHistory.Add(priceDataPoint);
            }
            PriceHistoryState = AppState.Loaded();
        }
        else
        {
            ErrorMessage = historyAsync.Message;
            PriceHistoryState = AppState.Error(historyAsync.Message);
        }
    }
    
    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
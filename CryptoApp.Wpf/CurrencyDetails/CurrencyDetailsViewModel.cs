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
    
    private string _baseId = string.Empty;
    public string BaseId
    {
        get => _baseId;
        set
        {
            if (_baseId == value) return;
            _baseId = value;
            OnPropertyChanged();
            _ = LoadCandlesAsync();
        }
    }
    
    private AppState _candlesState = AppState.Loading();
    public AppState CandlesState
    {
        get => _candlesState;
        private set
        {
            if (_candlesState == value) return;
            _candlesState = value;
            OnPropertyChanged();
        }
    }
    public List<CandleData> Candles { get; } = [];
    private async Task LoadCandlesAsync()
    {
        CandlesState = AppState.Loading();

        string firstDate = DateTimeOffset.UtcNow.AddDays(-1).ToUnixTimeMilliseconds().ToString();
        string secondDate = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString();
        var historyAsync = await _cryptoService.GetCryptoCurrencyHistoryAsync(
            exchange:"poloniex",
            interval: "h1",
            baseId: BaseId,
            quoteId: "bitcoin",
            start: firstDate,
            end: secondDate
            );
        
        if (historyAsync.IsSuccess)
        {
            if(!historyAsync.Data.Data.Any())
            {
                CandlesState = AppState.Error("No data available");
                return;
            }
            
            Candles.Clear();
            Candles.AddRange(historyAsync.Data.Data);
            CandlesState = AppState.Loaded();
        }
        else
        {
            CandlesState = AppState.Error(historyAsync.Message);
        }
    }
    
    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
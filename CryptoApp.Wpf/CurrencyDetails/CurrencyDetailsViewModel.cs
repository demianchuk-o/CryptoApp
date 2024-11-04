using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CryptoApp.Application.Crypto;
using CryptoApp.Core.CryptoCurrencies;
using CryptoApp.Wpf.Shared;
using CryptoApp.Wpf.Shared.Commands;
using CryptoApp.Wpf.Shared.Navigation;
using CryptoApp.Wpf.Shared.Navigation.Manager;

namespace CryptoApp.Wpf.CurrencyDetails;

public class CurrencyDetailsViewModel : INotifyPropertyChanged
{
    private readonly ICryptoService _cryptoService;
    private readonly IFrameNavigationManager _navigationManager;
    private INavigationService _navigationService;
    public CurrencyDetailsViewModel(ICryptoService cryptoService, IFrameNavigationManager navigationManager)
    {
        _cryptoService = cryptoService;
        _navigationManager = navigationManager;
    }

    public void Initialize(FrameType frameType)
    {
        _navigationService = _navigationManager.GetNavigationService(frameType);
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
            _ = LoadCurrencyAsync();
            _ = LoadMarketsAsync();
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

        string firstDate = DateTimeOffset.UtcNow.AddDays(-30).ToUnixTimeMilliseconds().ToString();
        string secondDate = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString();
        Console.WriteLine($"requesting {_baseId} candles from {firstDate} to {secondDate}");
        var historyAsync = await _cryptoService.GetCryptoCurrencyHistoryAsync(
            exchange:"coinbase",
            interval: "d1",
            baseId: BaseId,
            quoteId: "usd",
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
    private AppState _currencyState = AppState.Loading();
    public AppState CurrencyState
    {
        get => _currencyState;
        private set
        {
            if (_currencyState == value) return;
            _currencyState = value;
            OnPropertyChanged();
        }
    }

    private CryptoCurrency _currency;
    public CryptoCurrency Currency
    {
        get => _currency;
        private set
        {
            if (_currency == value) return;
            _currency = value;
            OnPropertyChanged();
        }
    }
    private async Task LoadCurrencyAsync()
    {
        CurrencyState = AppState.Loading();
        var result = await _cryptoService.GetCryptoCurrencyAsync(BaseId);
        
        if (result.IsSuccess)
        {
            Currency = result.Data;
            CurrencyState = AppState.Loaded();
        }
        else
        {
            CurrencyState = AppState.Error(result.Message);
        }
    }
    
    private AppState _marketsState = AppState.Loading();
    public AppState MarketsState
    {
        get => _marketsState;
        private set
        {
            if (_marketsState == value) return;
            _marketsState = value;
            OnPropertyChanged();
        }
    }

    public ObservableCollection<MarketPrice> Markets { get; } = [];
    private async Task LoadMarketsAsync()
    {
        MarketsState = AppState.Loading();
        var result = await _cryptoService.GetCryptoCurrencyMarketsAsync(BaseId);
        
        if (result.IsSuccess)
        {
            if(!result.Data.MarketPrices.Any())
            {
                MarketsState = AppState.Error("No data available");
                return;
            }
            
            Markets.Clear();
            foreach (var marketPrice in result.Data.MarketPrices)
            {
                Markets.Add(marketPrice);
            }
            MarketsState = AppState.Loaded();
        }
        else
        {
            MarketsState = AppState.Error(result.Message);
        }
    }
    private RelayCommand? _goBackCommand;
    public ICommand GoBackCommand => _goBackCommand ??= new RelayCommand(_ => _navigationService?.GoBack(),
        _ => _navigationService is not null && _navigationService.CanGoBack); 
    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
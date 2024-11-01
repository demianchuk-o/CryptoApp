using System.Collections.ObjectModel;
using System.ComponentModel;
using CryptoApp.Application.Crypto;
using CryptoApp.Core.CryptoCurrencies;
using CryptoApp.Wpf.Shared;
using CryptoApp.Wpf.Shared.Commands;

namespace CryptoApp.Wpf.TopCurrencies;

public class TopCurrenciesViewModel : INotifyPropertyChanged
{
    private readonly ICryptoService _cryptoService;
    public TopCurrenciesViewModel(ICryptoService cryptoService)
    {
        _cryptoService = cryptoService;
    }
    
    private AppState _appState;
    public AppState AppState
    {
        get => _appState;
        private set
        {
            if (_appState == value) return;
            _appState = value;
            OnPropertyChanged(nameof(AppState));
        }
    }
    private int _limit = 10;
    
    public int Limit
    {
        get => _limit;
        set
        {
            if (_limit == value) return;
            _limit = value > 0 ? value : 1;
            OnPropertyChanged(nameof(Limit));
        }
    }
    
    public ObservableCollection<CryptoCurrency> CryptoCurrencies { get; } = [];
    private RelayCommand? _loadDataCommand;
    public RelayCommand LoadDataCommand => _loadDataCommand ??= new RelayCommand(async _ => await LoadDataAsync());
    public async Task LoadDataAsync()
    {
        AppState = AppState.Loading();
        var result = await _cryptoService.GetCryptoCurrenciesAsync(Limit);
        if (result.IsSuccess)
        {
            CryptoCurrencies.Clear();
            foreach (var cryptoCurrency in result.Data)
            {
                CryptoCurrencies.Add(cryptoCurrency);
            }
            AppState = AppState.Loaded();
        }
        else
        {
            AppState = AppState.Error(result.Message);
        }
    }
    
    public event PropertyChangedEventHandler? PropertyChanged;
    public void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
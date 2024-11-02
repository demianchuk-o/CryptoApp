﻿using System.Collections.ObjectModel;
using System.ComponentModel;
using CryptoApp.Application.Crypto;
using CryptoApp.Core.CryptoCurrencies;
using CryptoApp.Wpf.Shared;
using CryptoApp.Wpf.Shared.Commands;

namespace CryptoApp.Wpf.SearchCurrencies;

public class SearchCurrenciesViewModel : INotifyPropertyChanged
{
    private readonly ICryptoService _cryptoService;

    public SearchCurrenciesViewModel(ICryptoService cryptoService)
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
    private string _searchTerm = "";
    public string SearchTerm
    {
        get => _searchTerm;
        set
        {
            if (_searchTerm == value) return;
            _searchTerm = value;
            OnPropertyChanged(nameof(SearchTerm));
        }
    }
    
    private RelayCommand? _searchCommand;
    public RelayCommand SearchCommand => _searchCommand ??= new RelayCommand(async _ => SearchAsync());

    private async Task SearchAsync()
    {
        if(string.IsNullOrWhiteSpace(SearchTerm)) return;
        
        AppState = AppState.Loading();
        var result = await _cryptoService.GetCryptoCurrenciesAsync(0, SearchTerm);
        if (result.IsSuccess)
        {
            CryptoCurrencies.Clear();
            AppState = AppState.Loaded();
            foreach (var cryptoCurrency in result.Data)
            {
                CryptoCurrencies.Add(cryptoCurrency);
            }
        }
        else
        {
            AppState = AppState.Error(result.Message);
        }
    }
    public ObservableCollection<CryptoCurrency> CryptoCurrencies { get; } = [];
    
    public event PropertyChangedEventHandler? PropertyChanged;
    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
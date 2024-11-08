﻿using CryptoApp.Core.CryptoCurrencies;
using CryptoApp.Core.Results;

namespace CryptoApp.Application.Crypto;

public interface ICryptoService
{
    Task<Result<List<CryptoCurrency>>> GetCryptoCurrenciesAsync(int limit = 0, string search = "");
    Task<Result<CryptoCurrency>> GetCryptoCurrencyAsync(string id);
    Task<Result<CryptoCurrencyMarkets>> GetCryptoCurrencyMarketsAsync(string id);
    Task<Result<CryptoCurrencyCandles>> GetCryptoCurrencyHistoryAsync(string exchange, string interval, string baseId, string quoteId, string? start = null, string? end = null);
}
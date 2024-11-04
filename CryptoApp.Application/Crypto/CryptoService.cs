using CryptoApp.Application.Mappers;
using CryptoApp.Core.CryptoCurrencies;
using CryptoApp.Core.Results;
using CryptoApp.Infrastructure.API.CoinCap;

namespace CryptoApp.Application.Crypto;

public class CryptoService : ICryptoService
{
    private readonly ICoinCapApiClient _coinCapApiClient;

    public CryptoService(ICoinCapApiClient coinCapApiClient)
    {
        _coinCapApiClient = coinCapApiClient;
    }

    public async Task<Result<List<CryptoCurrency>>> GetCryptoCurrenciesAsync(int limit = 0, string search = "")
    {
        var result = await _coinCapApiClient.GetAssetsAsync(limit, search);
        if (result.IsSuccess)
        {
            var responseData = result.Data;
            var cryptoCurrencies = responseData.Data
                .Select(CryptoCurrencyMapper.Map)
                .ToList();
            
            return Result<List<CryptoCurrency>>.Success(cryptoCurrencies);
        }
        
        return Result<List<CryptoCurrency>>.Failure(result.Message);
    }

    public async Task<Result<CryptoCurrency>> GetCryptoCurrencyAsync(string id)
    {
        var result = await _coinCapApiClient.GetAssetAsync(id);
        if (result.IsSuccess)
        {
            var responseData = result.Data;
            var cryptoCurrency = CryptoCurrencyMapper.Map(responseData.Data);
            
            return Result<CryptoCurrency>.Success(cryptoCurrency);
        }
        return Result<CryptoCurrency>.Failure(result.Message);
    }

    public async Task<Result<CryptoCurrencyMarkets>> GetCryptoCurrencyMarketsAsync(string id)
    {
        var result = await _coinCapApiClient.GetMarketsAsync(id);
        if (result.IsSuccess)
        {
            var responseData = result.Data;
            var cryptoCurrencyMarkets = CryptoCurrencyMarketsMapper.Map(responseData);
            
            return Result<CryptoCurrencyMarkets>.Success(cryptoCurrencyMarkets);
        }
        return Result<CryptoCurrencyMarkets>.Failure(result.Message);
    }

    public async Task<Result<CryptoCurrencyCandles>> GetCryptoCurrencyHistoryAsync(string exchange, string interval, string baseId, string quoteId, string? start = null, string? end = null)
    {
        var result = await _coinCapApiClient.GetCandlesAsync(exchange, interval, baseId, quoteId, start, end);
        if (result.IsSuccess)
        {
            var responseData = result.Data;
            var cryptoCurrencyHistory = CryptoCurrencyHistoryMapper.Map(responseData);
            
            return Result<CryptoCurrencyCandles>.Success(cryptoCurrencyHistory);
        }
        return Result<CryptoCurrencyCandles>.Failure(result.Message);
    }
}
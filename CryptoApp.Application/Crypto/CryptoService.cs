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

    public async Task<Result<CryptoCurrencyHistory>> GetCryptoCurrencyHistoryAsync(string id, string interval, string start, string end)
    {
        var result = await _coinCapApiClient.GetHistoryAsync(id, interval, start, end);
        if (result.IsSuccess)
        {
            var responseData = result.Data;
            var cryptoCurrencyHistory = CryptoCurrencyHistoryMapper.Map(responseData);
            
            return Result<CryptoCurrencyHistory>.Success(cryptoCurrencyHistory);
        }
        return Result<CryptoCurrencyHistory>.Failure(result.Message);
    }
}
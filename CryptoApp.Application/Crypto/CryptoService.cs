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

    public async Task<Result<List<CryptoCurrency>>> GetTopCryptoCurrenciesAsync(int limit, string search)
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
}
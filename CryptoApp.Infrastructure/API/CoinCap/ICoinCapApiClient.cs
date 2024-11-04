using System.Globalization;
using CryptoApp.Core.Results;
using CryptoApp.Infrastructure.API.CoinCap.Responses;

namespace CryptoApp.Infrastructure.API.CoinCap;

public interface ICoinCapApiClient
{
    public Task<Result<CoinCapGetAssetsResponse>> GetAssetsAsync(int limit, string search);
    public Task<Result<CoinCapGetAssetResponse>> GetAssetAsync(string id);
    public Task<Result<CoinCapGetMarketsResponse>> GetMarketsAsync(string id);
    public Task<Result<CoinCapGetCandlesResponse>> GetCandlesAsync(
        string exchange, string interval, string baseId, string quoteId, string? start = null, string? end = null);
}
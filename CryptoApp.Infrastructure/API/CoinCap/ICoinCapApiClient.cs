using CryptoApp.Core.Results;
using CryptoApp.Infrastructure.API.CoinCap.Responses;

namespace CryptoApp.Infrastructure.API.CoinCap;

public interface ICoinCapApiClient
{
    public Task<Result<CoinCapGetAssetsResponse>> GetAssetsAsync(int limit, string search);
    public Task<Result<CoinCapGetHistoryResponse>> GetHistoryAsync(string id, string interval, string start, string end);
}
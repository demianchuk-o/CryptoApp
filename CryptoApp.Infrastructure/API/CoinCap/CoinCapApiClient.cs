using System.Text.Json;
using CryptoApp.Core.Results;
using CryptoApp.Infrastructure.API.CoinCap.Responses;

namespace CryptoApp.Infrastructure.API.CoinCap;

public class CoinCapApiClient : ICoinCapApiClient
{
    private readonly HttpClient HttpClient;
    private readonly string _baseUrl = "https://api.coincap.io/v2/";
    public CoinCapApiClient(HttpClient httpClient)
    {
        httpClient.BaseAddress = new Uri(_baseUrl);
        HttpClient = httpClient;
    }
    
    public Task<Result<CoinCapGetAssetsResponse>> GetAssetsAsync(int limit)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"assets?limit={limit}");
        var response = HttpClient.SendAsync(request);
        if (response.Result.IsSuccessStatusCode)
        {
            var content = response.Result.Content.ReadAsStringAsync();
            var data = JsonSerializer.Deserialize<CoinCapGetAssetsResponse>(content.Result);
            return Task.FromResult(Result<CoinCapGetAssetsResponse>.Success(data));
        }

        return Task.FromResult(Result<CoinCapGetAssetsResponse>.Failure("Failed to get assets"));
    }
}
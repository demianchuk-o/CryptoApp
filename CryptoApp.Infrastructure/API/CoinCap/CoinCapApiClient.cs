using System.Text.Json;
using System.Text.Json.Serialization;
using CryptoApp.Core.Results;
using CryptoApp.Infrastructure.API.CoinCap.Responses;
using CryptoApp.Infrastructure.API.Shared.RateLimiter;

namespace CryptoApp.Infrastructure.API.CoinCap;

public class CoinCapApiClient : ICoinCapApiClient
{
    private readonly HttpClient HttpClient;
    private readonly RateLimiter _rateLimiter = new RateLimiter(TimeSpan.FromMilliseconds(500));
    private readonly string _baseUrl = "https://api.coincap.io/v2/";
    private static readonly JsonSerializerOptions _serializerOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        NumberHandling = JsonNumberHandling.AllowReadingFromString 
                         | JsonNumberHandling.WriteAsString
    };
    public CoinCapApiClient(HttpClient httpClient)
    {
        httpClient.BaseAddress = new Uri(_baseUrl);
        HttpClient = httpClient;
    }
    
    public async Task<Result<CoinCapGetAssetsResponse>> GetAssetsAsync(int limit = 0, string search = "")
    {
        List<string> queryParameters = [];
        if (limit > 0)
        {
            queryParameters.Add($"limit={limit}");
        }
        if (!string.IsNullOrWhiteSpace(search))
        {
            queryParameters.Add($"search={search}");
        }
        
        return await GetAsync<CoinCapGetAssetsResponse>("assets", queryParameters);
    }

    public async Task<Result<CoinCapGetAssetResponse>> GetAssetAsync(string id)
    {
        return await GetAsync<CoinCapGetAssetResponse>($"assets/{id}", []); 
    }


    public async Task<Result<CoinCapGetCandlesResponse>> GetCandlesAsync(string exchange, string interval, string baseId, string quoteId, string? start = null,
        string? end = null)
    {
       List<string> queryParameters =
        [
            $"exchange={exchange}",
            $"interval={interval}",
            $"baseId={baseId}",
            $"quoteId={quoteId}"
        ];
        
        if (!string.IsNullOrWhiteSpace(start))
        {
            queryParameters.Add($"start={start}");
        }
        
        if (!string.IsNullOrWhiteSpace(end))
        {
            queryParameters.Add($"end={end}");
        }
        
        return await GetAsync<CoinCapGetCandlesResponse>("candles", queryParameters);
    }
    
    private async Task<Result<T>> GetAsync<T>(string endpoint, List<string> queryParameters) where T : class
    {
        
        var queryString = queryParameters.Any()
            ? "?" + string.Join("&", queryParameters)
            : "";
        
        var request = new HttpRequestMessage(HttpMethod.Get, $"{endpoint}{queryString}");
        try
        {
            var response = await HttpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            
            var content = await response.Content.ReadAsStringAsync();
            var data = JsonSerializer.Deserialize<T>(content, _serializerOptions);
            
            return Result<T>.Success(data);
        }
        catch (HttpRequestException e)
        {
            return Result<T>.Failure($"API request failed: {e.Message}");
        }
        catch (JsonException e)
        {
            return Result<T>.Failure($"Failed to parse JSON: {e.Message}");
        }
        catch (Exception e)
        {
            return Result<T>.Failure($"An error occurred: {e.Message}");
        }
    }
}
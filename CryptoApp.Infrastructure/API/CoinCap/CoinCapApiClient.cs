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
        if(_rateLimiter.CanProcess() is false)
        {
            return Result<CoinCapGetAssetsResponse>.Failure("Request ignored to not exceed rate limit");
        }
        
        List<string> queryParameters = [];
        if (limit > 0)
        {
            queryParameters.Add($"limit={limit}");
        }
        if (!string.IsNullOrWhiteSpace(search))
        {
            queryParameters.Add($"search={search}");
        }
        var queryString = queryParameters.Any() 
            ? "?" + string.Join("&", queryParameters)
            : "";
        
        var request = new HttpRequestMessage(HttpMethod.Get, $"assets{queryString}");
        try
        {
            var response = await HttpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            
            var data = JsonSerializer.Deserialize<CoinCapGetAssetsResponse>(content, _serializerOptions);
    
            return Result<CoinCapGetAssetsResponse>.Success(data);
        }
        catch (HttpRequestException e)
        {
            return Result<CoinCapGetAssetsResponse>.Failure($"API request failed: {e.Message}");
        }
        catch (JsonException e)
        {
            return Result<CoinCapGetAssetsResponse>.Failure($"Failed to parse JSON: {e.Message}");
        }
        catch (Exception e)
        {
            return Result<CoinCapGetAssetsResponse>.Failure($"An error occurred: {e.Message}");
        }
        
    }

    public async Task<Result<CoinCapGetHistoryResponse>> GetHistoryAsync(string id, string interval, string start, string end)
    {
        if(_rateLimiter.CanProcess() is false)
        {
            return Result<CoinCapGetHistoryResponse>.Failure("Request ignored to not exceed rate limit");
        }
        
        var request = new HttpRequestMessage(HttpMethod.Get, $"assets/{id}/history?interval={interval}&start={start}&end={end}");
        try
        {
            var response = await HttpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            
            var content = await response.Content.ReadAsStringAsync();
            var data = JsonSerializer.Deserialize<CoinCapGetHistoryResponse>(content, _serializerOptions);
            
            return Result<CoinCapGetHistoryResponse>.Success(data);
        }
        catch (HttpRequestException e)
        {
            return Result<CoinCapGetHistoryResponse>.Failure($"API request failed: {e.Message}");
        }
        catch (JsonException e)
        {
            return Result<CoinCapGetHistoryResponse>.Failure($"Failed to parse JSON: {e.Message}");
        }
        catch (Exception e)
        {
            return Result<CoinCapGetHistoryResponse>.Failure($"An error occurred: {e.Message}");
        }
    }
}
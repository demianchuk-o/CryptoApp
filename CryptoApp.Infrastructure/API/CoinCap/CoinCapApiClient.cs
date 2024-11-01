using System.Text.Json;
using System.Text.Json.Serialization;
using CryptoApp.Core.Results;
using CryptoApp.Infrastructure.API.CoinCap.Responses;

namespace CryptoApp.Infrastructure.API.CoinCap;

public class CoinCapApiClient : ICoinCapApiClient
{
    private readonly HttpClient HttpClient;
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
}
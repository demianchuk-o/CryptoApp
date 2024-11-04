namespace CryptoApp.Infrastructure.API.CoinCap.Responses;

public record CoinCapGetCandlesResponse(
    CandlePoint[] Data
    );
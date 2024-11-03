namespace CryptoApp.Infrastructure.API.CoinCap.Responses;

public record CoinCapGetHistoryResponse(
    CoinCapPriceDataPoint[] Data
    );
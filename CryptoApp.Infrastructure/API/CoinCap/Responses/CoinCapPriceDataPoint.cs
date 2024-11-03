namespace CryptoApp.Infrastructure.API.CoinCap.Responses;

public record CoinCapPriceDataPoint(
    int PriceUsd,
    long Time
    );
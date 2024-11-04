namespace CryptoApp.Infrastructure.API.CoinCap.Responses;

public record CoinCapPriceDataPoint(
    decimal PriceUsd,
    long Time
    );
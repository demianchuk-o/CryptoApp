namespace CryptoApp.Infrastructure.API.CoinCap.Responses;

public record CandlePoint(
    decimal Open,
    decimal High,
    decimal Low,
    decimal Close,
    decimal Volume,
    long Period
    );
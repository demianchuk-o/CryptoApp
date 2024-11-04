namespace CryptoApp.Infrastructure.API.CoinCap.Responses;

public record AssetMarket(
    string ExchangeId,
    string BaseId,
    decimal PriceUsd);
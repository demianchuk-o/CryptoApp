namespace CryptoApp.Infrastructure.API.CoinCap.Responses;

public record AssetMarket(
    string exchangeId,
    string baseId,
    decimal priceUsd);
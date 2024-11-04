namespace CryptoApp.Infrastructure.API.CoinCap.Responses;

public record CoinCapGetMarketsResponse(
    AssetMarket[] data);

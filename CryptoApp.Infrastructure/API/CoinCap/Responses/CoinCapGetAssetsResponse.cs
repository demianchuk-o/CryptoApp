namespace CryptoApp.Infrastructure.API.CoinCap.Responses;

public record struct CoinCapGetAssetsResponse(
    CoinCapAsset[] Data,
    int Timestamp
);
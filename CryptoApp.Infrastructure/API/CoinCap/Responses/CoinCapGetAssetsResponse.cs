namespace CryptoApp.Infrastructure.API.CoinCap.Responses;

public record  CoinCapGetAssetsResponse(
    CoinCapAsset[] Data,
    long Timestamp
);
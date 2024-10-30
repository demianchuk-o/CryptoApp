namespace CryptoApp.Infrastructure.API.CoinCap.Responses;

public record  CoinCapGetAssetsResponse(
    CoinCapAsset[] Data,
    int Timestamp
);
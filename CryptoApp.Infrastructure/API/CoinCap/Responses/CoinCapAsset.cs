namespace CryptoApp.Infrastructure.API.CoinCap.Responses;

public record struct CoinCapAsset(
    string Id,
    int Rank,
    string Symbol,
    string Name,
    decimal Supply,
    decimal? MaxSupply,
    decimal MarketCapUsd,
    decimal VolumeUsd24Hr,
    decimal PriceUsd,
    decimal? ChangePercent24Hr,
    decimal Vwap24Hr
);




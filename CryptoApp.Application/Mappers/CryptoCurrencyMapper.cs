using CryptoApp.Core.CryptoCurrencies;
using CryptoApp.Infrastructure.API.CoinCap.Responses;

namespace CryptoApp.Application.Mappers;

public static class CryptoCurrencyMapper
{
    public static CryptoCurrency Map(CoinCapAsset asset)
    {
        return new CryptoCurrency
        {
            Id = asset.Id,
            Name = asset.Name,
            Symbol = asset.Symbol,
            Rank = asset.Rank,
            PriceUsd = asset.PriceUsd,
            MarketCapUsd = asset.MarketCapUsd,
            VolumeUsd24Hr = asset.VolumeUsd24Hr,
            ChangePercent24Hr = asset.ChangePercent24Hr,
        };
    }
}
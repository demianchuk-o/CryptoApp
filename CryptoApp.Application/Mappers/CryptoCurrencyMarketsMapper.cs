using CryptoApp.Core.CryptoCurrencies;
using CryptoApp.Infrastructure.API.CoinCap.Responses;

namespace CryptoApp.Application.Mappers;

public static class CryptoCurrencyMarketsMapper
{
    public static CryptoCurrencyMarkets Map(CoinCapGetMarketsResponse response)
    {
        var marketPrices = response.Data.Select(x => new MarketPrice
        {
            ExchangeId = x.ExchangeId,
            CurrencyId = x.BaseId,
            PriceUsd = x.PriceUsd
        }).ToArray();

        return new CryptoCurrencyMarkets
        {
            MarketPrices = marketPrices
        };
    }
}
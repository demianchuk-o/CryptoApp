using CryptoApp.Core.CryptoCurrencies;
using CryptoApp.Infrastructure.API.CoinCap.Responses;

namespace CryptoApp.Application.Mappers;

public static class CryptoCurrencyHistoryMapper
{
    public static CryptoCurrencyHistory Map(CoinCapGetHistoryResponse response)
    {
        return new CryptoCurrencyHistory
        {
            Data = response.Data.Select(dataPoint => new PriceDataPoint
            {
                PriceUsd = dataPoint.PriceUsd,
                Time = DateTimeOffset.FromUnixTimeMilliseconds(dataPoint.Time).DateTime
            }).ToArray()
        };
    }
}
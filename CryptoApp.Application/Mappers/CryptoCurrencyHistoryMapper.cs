using CryptoApp.Core.CryptoCurrencies;
using CryptoApp.Infrastructure.API.CoinCap.Responses;

namespace CryptoApp.Application.Mappers;

public static class CryptoCurrencyHistoryMapper
{
    public static CryptoCurrencyCandles Map(CoinCapGetCandlesResponse response)
    {
        return new CryptoCurrencyCandles
        {
            Data = response.Data.Select(dataPoint => new CandleData
            {
                Open = dataPoint.Open,
                Close = dataPoint.Close,
                High = dataPoint.High,
                Low = dataPoint.Low,
                Volume = dataPoint.Volume,
                Period = DateTimeOffset.FromUnixTimeMilliseconds(dataPoint.Period).DateTime
            }).ToArray()
        };
    }
}
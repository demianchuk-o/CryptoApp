namespace CryptoApp.Core.CryptoCurrencies;

public class MarketPrice
{
    public string ExchangeId { get; set; }
    public string CurrencyId { get; set; }
    public decimal PriceUsd { get; set; }
}
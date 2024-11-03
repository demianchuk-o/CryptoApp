using CryptoApp.Core.CryptoCurrencies;
using CryptoApp.Core.Results;

namespace CryptoApp.Application.Crypto;

public interface ICryptoService
{
    Task<Result<List<CryptoCurrency>>> GetCryptoCurrenciesAsync(int limit = 0, string search = "");
    Task<Result<CryptoCurrencyHistory>> GetCryptoCurrencyHistoryAsync(string id, string interval, string start, string end);
}
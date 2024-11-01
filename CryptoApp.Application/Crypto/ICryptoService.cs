using CryptoApp.Core.CryptoCurrencies;
using CryptoApp.Core.Results;

namespace CryptoApp.Application.Crypto;

public interface ICryptoService
{
    Task<Result<List<CryptoCurrency>>> GetCryptoCurrenciesAsync(int limit, string search = "");
}
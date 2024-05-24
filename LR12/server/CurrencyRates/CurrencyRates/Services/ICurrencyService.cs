using CurrencyRates.Models;

namespace CurrencyRates.Services {
    public interface ICurrencyService {
        public Task<CurrencyRate> GetCurrencyRate(string currency);
    }
}

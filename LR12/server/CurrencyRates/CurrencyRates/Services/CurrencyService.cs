using CurrencyRates.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace CurrencyRates.Services {
    public class CurrencyService : ICurrencyService {
        private readonly HttpClient _httpClient;

        public CurrencyService(HttpClient httpClient) {
            _httpClient = httpClient;
        }

        public async Task<CurrencyRate> GetCurrencyRate(string currency) {
            var response = await _httpClient.GetStringAsync($"https://api.currencyapi.com/v3/latest?apikey=WHY_YOU_LOOKING_HERE&base_currency=UAH&currencies={currency}");

            var json = JObject.Parse(response);

            if (!json["data"].HasValues || json["data"][currency] == null) {
                throw new Exception($"Currency rate for {currency} not found in the response.");
            }

            var rate = json["data"][currency]["value"].Value<decimal>();
            return new CurrencyRate { Currency = currency, Rate = rate };
        }
    }
}

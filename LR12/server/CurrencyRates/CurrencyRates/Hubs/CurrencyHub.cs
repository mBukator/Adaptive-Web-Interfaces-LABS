using CurrencyRates.Models;
using Microsoft.AspNetCore.SignalR;

namespace CurrencyRates.Hubs {
    public class CurrencyHub : Hub {
        public async Task SendCurrencyRate(CurrencyRate rate) {
            await Clients.All.SendAsync("ReceiveCurrencyRate", rate);
        }
    }
}

using CurrencyRates.Hubs;
using CurrencyRates.Models;
using CurrencyRates.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace CurrencyRates.Controllers {
    public class CurrencyController : ControllerBase {
        private readonly IHubContext<CurrencyHub> _hubContext;
        private readonly CurrencyService _currencyService;

        public CurrencyController(IHubContext<CurrencyHub> hubContext, CurrencyService currencyService) {
            _hubContext = hubContext;
            _currencyService = currencyService;
        }

        [HttpGet("get-currency-rate")]
        public async Task<IActionResult> GetCurrencyRate([FromQuery] string currency) {
            var currencyRate = await _currencyService.GetCurrencyRate(currency);
            await _hubContext.Clients.All.SendAsync("ReceiveCurrencyRate", currencyRate);
            return Ok(currencyRate);

        }
    }
}

using ExchangeApi.Models;
using ExchangeApi.Services.Interfaces;
using ExchangeShared;
using Microsoft.AspNetCore.Mvc;

namespace ExchangeApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TradeController : ControllerBase
{
    private readonly ITradeService _tradeService;

    public TradeController(ITradeService tradeService)
    {
        _tradeService = tradeService;
    }

    [HttpGet]
    public async Task<IEnumerable<Trade>> GetMostRecentTrades([FromQuery] TradeRequest request)
    {
        return await _tradeService.GetMostRecentTrades(request);
    }
}
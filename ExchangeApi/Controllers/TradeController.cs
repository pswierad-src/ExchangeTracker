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

    [HttpGet("mostRecentTrades")]
    public async Task<IEnumerable<Trade>> GetMostRecentTradesAsync([FromQuery] TradeRequest request)
    {
        return await _tradeService.GetMostRecentTrades(request);
    }

    [HttpGet("tradeLog")]
    [ProducesResponseType(typeof(IEnumerable<TradeLogResponse>), StatusCodes.Status200OK)]
    public async Task<IEnumerable<TradeLogResponse>> GetTradeLogAsync([FromQuery] int amount = 10000)
    {
        return await _tradeService.GetTradeLog(amount);
    }
}
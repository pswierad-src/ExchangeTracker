using ExchangeApi.Models;
using ExchangeShared;

namespace ExchangeApi.Services.Interfaces;

public interface ITradeService
{
    Task<IEnumerable<Trade>> GetMostRecentTrades(TradeRequest request);
    Task<IEnumerable<TradeLogResponse>> GetTradeLog();
}
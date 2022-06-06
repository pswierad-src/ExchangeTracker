using ExchangeShared;

namespace ExchangeApi.Models;

public class TradeRequest
{
    public TradeType? Type { get; set; }
    public int Amount { get; set; }
}
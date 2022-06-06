using ExchangeShared;

namespace ExchangeApi.Models;

public class TradeLogResponse
{
    public Guid ExchangeId { get; set; }
    public string ExchangedProduct { get; set; }
    public decimal Quantity { get; set; }
    public DateTime DateOfOperation { get; set; }
    
}
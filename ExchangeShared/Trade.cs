namespace ExchangeShared;

public class Trade
{
    public Guid ExchangeId { get; set; }
    public TradeType Type { get; set; }
    public string ExchangedProduct { get; set; }
    public decimal Quantity { get; set; }
    public Guid Seller { get; set; }
    public Guid Buyer { get; set; }
    public DateTime DateOfOperation { get; set; }
}
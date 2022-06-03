using ExchangeShared;

namespace ExchangeMongo.Documents;

public class TradeDocument : IDocument
{
    public Guid Id { get; init; }
    public TradeType Type { get; init; }
    public string ExchangedProduct { get; init; }
    public decimal Quantity { get; init; }
    public Guid Seller { get; init; }
    public Guid Buyer { get; init; }
    public DateTime DateOfOperation { get; init; }
}
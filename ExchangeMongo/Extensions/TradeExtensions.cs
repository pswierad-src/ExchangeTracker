using ExchangeMongo.Documents;
using ExchangeShared;

namespace ExchangeMongo.Extensions;

public static class TradeExtensions
{
    public static TradeDocument ToDocument(this Trade trade)
    {
        return new TradeDocument
        {
            Id = trade.ExchangeId,
            Type = trade.Type,
            ExchangedProduct = trade.ExchangedProduct,
            Quantity = trade.Quantity,
            Buyer = trade.Buyer,
            Seller = trade.Seller,
            DateOfOperation = trade.DateOfOperation
        };
    }

    public static Trade ToDomainObject(this TradeDocument document)
    {
        return new Trade
        {
            ExchangeId = document.Id,
            Type = document.Type,
            ExchangedProduct = document.ExchangedProduct,
            Quantity = document.Quantity,
            Buyer = document.Buyer,
            Seller = document.Seller,
            DateOfOperation = document.DateOfOperation
        };
    }
}
namespace ExchangeShared;

public class TradeProvider : ITradeProvider
{
    private readonly List<string> Currencies = new() { "PLN", "USD", "GBP", "EUR", "CHF" };
    private readonly List<string> Cryptos = new() { "BTC", "ETH", "BNB", "USDT", "ADA" };
    private readonly List<string> Stocks = new() { "S&P 500", "WIG20", "GOOG", "AAPL", "TSLA" };

    private readonly List<string> Assets = new()
        { "apartments", "cars", "graphic cards", "1k gold bars", "oil barrel" };

    public Trade GenerateTrade()
    {
        var vals = Enum.GetValues(typeof(TradeType));
        var type = (TradeType)vals.GetValue(new Random().Next(vals.Length))!;

        return new Trade
        {
            ExchangeId = Guid.NewGuid(),
            Buyer = Guid.NewGuid(),
            Seller = Guid.NewGuid(),
            DateOfOperation = DateTime.UtcNow,
            Type = type,
            ExchangedProduct = GetExchangedProduct(type),
            Quantity = GetRandomQuantity(type)
        };
    }

    private string GetExchangedProduct(TradeType type)
        => type switch
        {
            TradeType.Currency => Currencies[new Random().Next(Currencies.Count)],
            TradeType.Cryptocurrency => Cryptos[new Random().Next(Cryptos.Count)],
            TradeType.Stocks => Stocks[new Random().Next(Stocks.Count)],
            TradeType.Assets => Assets[new Random().Next(Assets.Count)],
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };

    private decimal GetRandomQuantity(TradeType type)
        => type switch
        {
            TradeType.Currency => (decimal)new Random().NextDouble() * 10000,
            TradeType.Cryptocurrency => (decimal)new Random().NextDouble() * 10000,
            TradeType.Stocks => new Random().Next(10000),
            TradeType.Assets => new Random().Next(10000),
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
}
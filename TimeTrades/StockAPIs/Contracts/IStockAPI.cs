using TimeTrades.Models;

namespace TimeTrades.StockAPIs.Contracts
{
    public interface IStockAPI
    {
        public string APIKey { get; }
        public IEnumerable<TimeSpan> Intervals { get; }
        public IEnumerable<Symbol> SupportedSymbols { get; }
    }
}

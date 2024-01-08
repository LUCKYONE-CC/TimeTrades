using System.Linq;
using TimeTrades.Models;
using TimeTrades.StockAPIs.Contracts;

namespace TimeTrades.StockAPIs
{
    public class AlphaStockAPI : StockAPI
    {
        private readonly string apiKey;
        public AlphaStockAPI(string apiKey) : base(apiKey)
        {
            this.apiKey = apiKey;
        }
        public override List<Symbol> SupportedSymbols { get; }
        public override List<TimeSpan> SupportedIntervals { get; }

        public override IEnumerable<Symbol> GetSupportedSymbols()
        {
            List<Symbol> supportedSymbols = new List<Symbol>
            {
                new Symbol("AAPL", "Apple Inc.", "A", "USA", "USD", "123456789")
            };

            return supportedSymbols;
        }
        public override IEnumerable<TimeSpan> GetSupportedIntervals()
        {
            List<TimeSpan> supportedIntervals = new List<TimeSpan>
            {
                TimeSpan.FromMinutes(1),
                TimeSpan.FromMinutes(5),
                TimeSpan.FromMinutes(15),
                TimeSpan.FromMinutes(30),
                TimeSpan.FromHours(1),
                TimeSpan.FromHours(4),
                TimeSpan.FromHours(8),
                TimeSpan.FromHours(12),
                TimeSpan.FromDays(1),
                TimeSpan.FromDays(7),
                TimeSpan.FromDays(30),
                TimeSpan.FromDays(365)
            };

            return supportedIntervals;
        }

        public override void SubscribeToSymbols(List<Symbol> symbolsToSubscribe)
        {
            var validSymbols = symbolsToSubscribe.Intersect(GetSupportedSymbols()).ToList();

            validSymbols.ForEach(symbol =>
            {
                validSymbols.Add(symbol);
            });

            if (validSymbols.Count == 0)
            {
                throw new ArgumentException("No valid symbols selected.");
            }
        }
        protected override ExchangeData GetExchangeData(Symbol symbol)
        {
            ///Make your API-Call here
            return new ExchangeData();
        }
    }
}

﻿using System.Linq;
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
        protected override IEnumerable<Symbol> SubscribedSymbols { get; set; }
        protected override bool HasStarted { get; set; }
        public override decimal PricePerRequest => 1m;
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
                TimeSpan.FromSeconds(1),
                TimeSpan.FromSeconds(5),
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
            var validSymbols = new List<Symbol>();
            symbolsToSubscribe.ForEach(symbol =>
            {
                if (GetSupportedSymbols().Any(x => x.Name == symbol.Name))
                {
                    validSymbols.Add(symbol);
                }
            });

            if (validSymbols.Count == 0)
            {
                throw new ArgumentException("No valid symbols selected.");
            }

            SubscribedSymbols = validSymbols;
        }
        protected override ExchangeData GetRealTimeExchangeData(Symbol symbol)
        {
            ///Make your API-Call here
            return new ExchangeData() { Str = "Test"};
        }
    }
}
